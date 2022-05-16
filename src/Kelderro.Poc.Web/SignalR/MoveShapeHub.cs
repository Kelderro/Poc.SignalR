using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading;

namespace Kelderro.Poc.Web.SignalR
{
    public class MoveShapeHub : Hub
    {
        // Is set via the constructor on each creation
        private MoveShapeBroadcaster _broadcaster;
        public MoveShapeHub()
            : this(MoveShapeBroadcaster.Instance)
        {
        }

        public MoveShapeHub(MoveShapeBroadcaster broadcaster)
        {
            _broadcaster = broadcaster;
        }

        public void UpdateModel(MoveShapeModel clientModel)
        {
            clientModel.LastUpdatedBy = Context.ConnectionId;
            // Update the shape model within our broadcaster
            _broadcaster.UpdateShape(clientModel);
        }
    }
}