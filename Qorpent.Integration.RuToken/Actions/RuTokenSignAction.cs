using System;
using Qorpent.Mvc;
using Qorpent.Mvc.Binding;
using Qorpent.Security.Cryptography;
using Qorpent.Security.Cryptography.Sources;

namespace Qorpent.Integration.RuToken.Actions {
    /// <summary>
    ///     Действие подписи сертификата по запросу
    /// </summary>
    [Action("rutoken.sign", Role = "DEFAULT", Help = "Производит валидацию и подпись сертификата по запросу")]
    public class RuTokenSignAction : ActionBase {
        /// <summary>
        ///     Указывает на то, разрешена ли регистрация токенов
        /// </summary>
        public static readonly bool AllowRegister = false;
        /// <summary>
        ///     CRS file
        /// </summary>
        [Bind(Required = true)] public string Crs { get; set; }

        /// <summary>
        ///     Main process
        /// </summary>
        /// <returns></returns>
        protected override object MainProcess() {
            return new[] {SignCsr()};
        }

        private string SignCsr() {
            if (!AllowRegister) {
                throw new Exception("Registration is not allowed for this build!");
            }

            var cryptoProvider = GetCryptoProvider();
            var action = new CryptoProviderAction(
                CryptoProviderActionType.Sign,
                new CryptoProviderEntity(Crs, CryptoProviderFileType.Pem, CryptoProviderEntityPrivacy.Public)
            );

            var result = cryptoProvider.Execute(action);

            return result.Entity.EntityBody;
        }

        private CryptoProvider GetCryptoProvider() {
            return new CryptoProvider(
                new CryptoSourceExternalOpenSsl()
            );
        }
    }
}
