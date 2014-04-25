using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Qorpent.IoC;

namespace Qorpent.Integration.YouTrack.Tests {
    [TestFixture]
    public class YouTrackExceptionRegistryTest : CertainApiTestBase<IYouTrackGeneralIssueApi> {
        private YouTrackExceptionRegistry _registry;
        private YouTrackConnectionException _exception;

        public override void Setup() {
            base.Setup();
            _registry = new YouTrackExceptionRegistry();
            _exception = new YouTrackConnectionException("Как будто не можем подключиться", server, user, null, null);

            var c = new ComponentDefinition<IYouTrackConnection, YouTrackConnection>(Lifestyle.Transient, "Test.youtrack.connection");
            c.Set("_server", server);
            c.Set("_user", "Exreg");
            c.Set("_password", "gfhjkm#1");

            Container.Register(c);

            _registry.SetContainerContext(Container, null);
            _registry.ProjectName = "ER";
            _registry.ConnectionName = "Test";
        }

        [Test]
        public void Issue_Created_Test() {
            _registry.Send(_exception, ErrorLevel.Error, new { }, "Во время подключения к серверу возникла ошибка");
        }

        [Test]
        public void Unknown_Command_Test() {
            
            try {
                invalidmethod1();
                
            } catch (Exception ex) {
                _registry.Send(ex, ErrorLevel.Error, new {server2 = "ecot2", server3 = "ecot2", server = "localhost"},
                               "Во время подключения к серверу возникла ошибка");
            }
        }

        private void invalidmethod1() {
            invalidmethod2();
        }

        private void invalidmethod2() {
            var a = 0;
            var x = 2 / a;
        }
    }
}
