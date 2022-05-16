using System;
using Xunit;
using Microsoft.AspNet.SignalR.Hubs;
using Moq;
using System.Dynamic;
using Kelderro.Poc.Web;
using Kelderro.Poc.Web.SignalR;

namespace Kelderro.Poc.Web.Test.SignalR
{
	public class MoveShapeTest
	{
		public class MoveShapeHub
		{

		}

		public class UpdateModel
		{

		}
	}

	public class ChatHubTest
	{
		public class Send
		{
			// http://www.asp.net/signalr/overview/testing-and-debugging/unit-testing-signalr-applications
			[Fact]
			public void SendCalled()
			{
				var sendCalled = false;
				var hub = new ChatHub();
				var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
				hub.Clients = mockClients.Object;
				dynamic all = new ExpandoObject();
				all.addNewMessageToPage = new Action<string, string>((name, message) =>
				{
					sendCalled = true;
				});
				mockClients.Setup(m => m.All).Returns((ExpandoObject)all);
				hub.Send("TestUser", "TestMessage");
				Assert.True(sendCalled);
			}
		}
	}
}