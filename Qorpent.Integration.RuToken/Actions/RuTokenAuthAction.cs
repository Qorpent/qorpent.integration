using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Qorpent.IoC;
using Qorpent.Mvc;
using Qorpent.Mvc.Binding;
using Qorpent.Mvc.Security;
using Qorpent.Security.Cryptography;
using Qorpent.Security.Cryptography.Sources;
using Qorpent.Utils.Extensions;

namespace Qorpent.Integration.RuToken.Actions {
    /// <summary>
    /// 
    /// </summary>
    [Action("rutoken.auth", Role = "DEFAULT", Arm = "TOKEN")]
    public class RuTokenAuthAction : ActionBase {
        /// <summary>
        /// 
        /// </summary>
        public static List<object> Sessions = new List<object>();
        /// <summary>
        /// 
        /// </summary>
        public static List<KeyValuePair<string, long>> LastAccess = new List<KeyValuePair<string, long>>();
            /// <summary>
        /// 
        /// </summary>
        [Bind(Required = true)] public string CertId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Bind(Required = true)] public string Cms { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Bind(Required = true)] public string Salt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Inject(Name = "RuToken.Storage")]
        public IRuTokenUsersStorage UsersStorage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override object MainProcess() {
            var state = CheckCredentials();

            if (state.IsSuccess) {
                if (!BuiltInTokens.Logins.ContainsKey(state.Entity.EntityMetadata["Login"])) {
                    throw new Exception("Not registered in system");
                }

                var options = BuiltInTokens.Logins[state.Entity.EntityMetadata["Login"]].ToDict();

                if (!options["DefaultOn"].ToBool()) {
                    if (UsersStorage == null) {
                        throw new Exception("Users storage is null");
                    }

                    if (!UsersStorage.IsActivated(username:state.Entity.EntityMetadata["Login"])) {
                        throw new Exception("Is not activated");
                    }
                }
                var config = UsersStorage.GetConfig(username: state.Entity.EntityMetadata["Login"]);
                var username = config != null ? config.ToDict()["FakeUsername"].ToString() : state.Entity.EntityMetadata["Login"];

                SetupAuthCookie(ResolveLogin(username));
                var authObject = new {
                    auth = true,
                    certid = CertId,
                    Hash = state.Entity.EntityMetadata["Hash"],
                    Salt = Salt,
                    Role = BuiltInTokens.GetRole(options["TokenId"].ToString()),
                    Login = ResolveLogin(username),
                    SignInTimestamp = CurrentTimestamp()
                };

                ((MvcContext)Context).NativeAspContext.Response.SetCookie(new HttpCookie("Salt", Salt));

                RegisterSession(state.Entity.EntityMetadata["Login"], authObject);
                return authObject;
            }

            throw new Exception("Auth false");
        }
        /// <summary>
        ///     Возвращает текущий UNIX TIMESTAMP
        /// </summary>
        /// <returns>Текущий UNIX TIMESTAMP</returns>
        public static long CurrentTimestamp() {
            return (DateTime.UtcNow.Ticks - DateTime.Parse("01/01/1970 00:00:00").Ticks) / 10000000;
        }
        /// <summary>
        ///     Регистрирует внутреннюю сессию
        /// </summary>
        /// <param name="username">Внутреннее имя пользователя</param>
        /// <param name="session">Объект представления сессии</param>
        private void RegisterSession(string username, object session) {
            CleanUpSessions(username);
            CleanUpLastAccess(username);
            Sessions.Add(session);
            LastAccess.Add(new KeyValuePair<string, long>(ResolveLogin(username), CurrentTimestamp()));
        }
        /// <summary>
        ///     Зачищает все старые сессии пользователя по его юзернэйму
        /// </summary>
        /// <param name="username">Внутреннее имя пользователя</param>
        private void CleanUpSessions(string username) {
            Sessions.Where(
                _ => _.ToDict()["Login"].ToString() == ResolveLogin(username)
            ).ToList().DoForEach(
                _ => Sessions.Remove(_)
            );
        }
        /// <summary>
        ///     Зачищает лог доступа к системе по имени пользователя
        /// </summary>
        /// <param name="username">Внутреннее имя пользователя</param>
        private void CleanUpLastAccess(string username) {
            LastAccess.Where(
                _ => _.Key == ResolveLogin(username)
            ).ToList().DoForEach(
                _ => LastAccess.Remove(_)
            );
        }
        /// <summary>
        ///     Резольвит системный логин пользователя по его юзернэйму
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        /// <returns>Системный логин для аутентификации</returns>
        public static string ResolveLogin(string username) {
            return "local\\" + username;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="plogin"></param>
        private void SetupAuthCookie(string plogin) {
            var cookie = FormsAuthentication.GetAuthCookie(plogin, false);
            if (cookie.Domain == null) {
                cookie.Domain = ((MvcContext)Context).NativeAspContext.Request.Url.Host;
            }
            var domainparts = cookie.Domain.Split('.');
            if (domainparts.Length == 3) {
                cookie.Domain = domainparts[1] + "." + domainparts[2];
            }
            ((MvcContext)Context).NativeAspContext.Response.SetCookie(cookie);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private ICryptoProviderResult CheckCredentials() {

            var source =  @"MIME-Version: 1.0
Content-Disposition: attachment; filename=""smime.p7m""
Content-Type: application/x-pkcs7-mime; smime-type=enveloped-data; name=""smime.p7m""
Content-Transfer-Encoding: base64

" + Cms + @"
";
            var cryptoProvider = GetCryptoProvider();
            var action = new CryptoProviderAction(
                CryptoProviderActionType.Verify,
                new CryptoProviderEntity(source, CryptoProviderFileType.Pkcs7, CryptoProviderEntityPrivacy.Public)
            );

            action.Config["Salt"] = Salt;

            var result = cryptoProvider.Execute(action);

            return result;
        }

        private CryptoProvider GetCryptoProvider() {
            return new CryptoProvider(
                new CryptoSourceExternalOpenSsl()
            );
        }
    }
}
