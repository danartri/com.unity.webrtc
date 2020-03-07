using UnityEngine;

namespace Unity.WebRTC
{

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    public class AsyncOperationBase : CustomYieldInstruction
    {
        public RTCError Error { get; internal set; }

        public bool IsError { get; internal set; }
        public bool IsDone { get; internal set; }

        public override bool keepWaiting
        {
            get
            {
                if (IsDone)
                {
                    return false;
                }
                else
                {
                    return true;  
                }   
            }
        }

        internal void Done()
        {
            IsDone = true;
        }
    }

    public class RTCSessionDescriptionAsyncOperation : AsyncOperationBase
    {
        public RTCSessionDescription Desc { get; internal set; }
    }

    public class RTCSetSessionDescriptionAsyncOperation : AsyncOperationBase
    {
        internal RTCSetSessionDescriptionAsyncOperation(RTCPeerConnection connection)
        {
            connection.OnSetSessionDescriptionSuccess = () =>
            {
                IsError = false;
                this.Done();
            };
            connection.OnSetSessionDescriptionFailure = () =>
            {
                IsError = true;
                this.Done();
            };
        }
    }


    public class RTCIceCandidateRequestAsyncOperation : CustomYieldInstruction
    {
        public bool isError { get; private set;  }
        public RTCError error { get; private set; }
        public bool isDone { get; private set;  }

        public override bool keepWaiting
        {
            get
            {
                return isDone;
            }
        }

        public void Done()
        {
            isDone = true;
        }
    }
    public class RTCAsyncOperation : CustomYieldInstruction
    {
        public bool isError { get; private set; }
        public RTCError error { get; private set; }
        public bool isDone { get; private set; }

        public override bool keepWaiting
        {
            get
            {
                return isDone;
            }
        }

        public void Done()
        {
            isDone = true;
        }
    }
}
