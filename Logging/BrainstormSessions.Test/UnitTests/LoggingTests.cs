using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrainstormSessions.Api;
using BrainstormSessions.Controllers;
using BrainstormSessions.Core.Interfaces;
using BrainstormSessions.Core.Model;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BrainstormSessions.Test.UnitTests
{
    public class LoggingTests
    {
       private static void VerifyLogging<T>(Mock<ILogger<T>> logger, string expectedMessage, LogLevel expectedLogLevel = LogLevel.Debug, Times? times = null)
       {
           times ??= Times.Once();
       
           Func<object, Type, bool> state = (v, t) => v.ToString().CompareTo(expectedMessage) == 0;
       
           logger.Verify(
               x => x.Log(
                   It.Is<LogLevel>(l => l == expectedLogLevel),
                   It.IsAny<EventId>(),
                   It.Is<It.IsAnyType>((v, t) => state(v, t)),
                   It.IsAny<Exception>(),
                   It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), (Times)times);
       } 
        
        [Fact]
        public async Task HomeController_Index_LogInfoMessages()
        {
            // Arrange
            var mockRepo = new Mock<IBrainstormSessionRepository>();
            mockRepo.Setup(repo => repo.ListAsync())
                .ReturnsAsync(GetTestSessions());
            var mockLogger = new Mock<ILogger<HomeController>>();

            var controller = new HomeController(mockRepo.Object, mockLogger.Object);

            // Act
            var result = await controller.Index();

            // Assert
            VerifyLogging(mockLogger, "Expected info log", LogLevel.Information);
        }

        [Fact]
        public async Task HomeController_IndexPost_LogWarningMessage_WhenModelStateIsInvalid()
        {
            // Arrange
            var mockRepo = new Mock<IBrainstormSessionRepository>();
            mockRepo.Setup(repo => repo.ListAsync())
                .ReturnsAsync(GetTestSessions());
            
            var mockLogger = new Mock<ILogger<HomeController>>();
            
            var controller = new HomeController(mockRepo.Object, mockLogger.Object);
            controller.ModelState.AddModelError("SessionName", "Required");
            var newSession = new HomeController.NewSessionModel();

            // Act
            var result = await controller.Index(newSession);

            // Assert
            VerifyLogging(mockLogger, "Expected warning log", LogLevel.Warning);
        }

        [Fact]
        public async Task IdeasController_CreateActionResult_LogErrorMessage_WhenModelStateIsInvalid()
        {
            // Arrange & Act
            var mockRepo = new Mock<IBrainstormSessionRepository>();
            
            var mockLogger = new Mock<ILogger<IdeasController>>();
            
            var controller = new IdeasController(mockRepo.Object, mockLogger.Object);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = await controller.CreateActionResult(model: null);

            // Assert
            VerifyLogging(mockLogger, "Expected error log", LogLevel.Error);
        }

        [Fact]
        public async Task SessionController_Index_LogDebugMessages()
        {
            // Arrange
            int testSessionId = 1;
            var mockRepo = new Mock<IBrainstormSessionRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(testSessionId))
                .ReturnsAsync(GetTestSessions().FirstOrDefault(
                    s => s.Id == testSessionId));
            
            var mockLogger = new Mock<ILogger<SessionController>>();
            
            var controller = new SessionController(mockRepo.Object, mockLogger.Object);

            // Act
            var result = await controller.Index(testSessionId);

            // Assert
            VerifyLogging(mockLogger, "Expected debug log", LogLevel.Debug);
        }

        private List<BrainstormSession> GetTestSessions()
        {
            var sessions = new List<BrainstormSession>();
            sessions.Add(new BrainstormSession()
            {
                DateCreated = new DateTime(2016, 7, 2),
                Id = 1,
                Name = "Test One"
            });
            sessions.Add(new BrainstormSession()
            {
                DateCreated = new DateTime(2016, 7, 1),
                Id = 2,
                Name = "Test Two"
            });
            return sessions;
        }

    }
}
