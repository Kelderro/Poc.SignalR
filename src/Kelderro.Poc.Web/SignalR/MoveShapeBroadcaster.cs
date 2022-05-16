using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace Kelderro.Poc.Web.SignalR
{
    public class MoveShapeBroadcaster
    {
        private readonly static Lazy<MoveShapeBroadcaster> _instance = new Lazy<MoveShapeBroadcaster>(() => new MoveShapeBroadcaster());
        // We're going to broadcast to all clients a maximum of 25 times per second
        private readonly TimeSpan BroadcastInterval = TimeSpan.FromMilliseconds(40);
        private readonly IHubContext _hubContext;
        private Timer _broadcastLoop;
        private MoveShapeModel _model;
        private bool _modelUpdated;
        public MoveShapeBroadcaster()
        {
            // Save our hub context so we can easily use it 
            // to send to its connected clients
            _hubContext = GlobalHost.ConnectionManager.GetHubContext<MoveShapeHub>();
            _model = new MoveShapeModel();
            _modelUpdated = false;
            // Start the broadcast loop
            _broadcastLoop = new Timer(
                BroadcastShape,
                null,
                BroadcastInterval,
                BroadcastInterval);
        }
        public void BroadcastShape(object state)
        {
            // No need to send anything if our model hasn't changed
            if (_modelUpdated)
            {
                // This is how we can access the Clients property 
                // in a static hub method or outside of the hub entirely
                _hubContext.Clients.AllExcept(_model.LastUpdatedBy).updateShape(_model);
                _modelUpdated = false;
            }
        }
        public void UpdateShape(MoveShapeModel clientModel)
        {
            _model = clientModel;
            _modelUpdated = true;
        }
        public static MoveShapeBroadcaster Instance
        {
            get
            {
                return _instance.Value;
            }
        }
    }
}