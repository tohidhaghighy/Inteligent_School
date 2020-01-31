using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using SchoolIntelligentWeb.Models;

namespace SchoolIntelligentWeb.Hubs
{
    public class WebRtcHub : Hub
    {
        public async Task Join(string username, string groupname)
        {
            Users.Add(new User
            {
                Username = username,
                ConnectionId = Context.ConnectionId,
                Group = groupname
            });
            await this.Groups.Add(this.Context.ConnectionId, groupname);
            string lists = makelistwithpermition(groupname);
            this.Clients.OthersInGroup(groupname).usercount(Users.Count());
            this.Clients.OthersInGroup(groupname).additem(lists);
            this.Clients.Caller.additem(lists);
            this.Clients.Caller.usercount(Users.Count());
            
        }
        public string makelist(string groupname)
        {
            string uri = "";
            var find = Users.Where(a => a.Group == groupname).ToList();
            foreach (var item in find)
            {
                uri += "<ul class='list-inline' style='padding-left:30px;'><li style='padding-left:5px; padding-top:5px;'><img src='/Scripts/Chatroom/Script/video-camera.png' style='height:20px;' class='center img-responsive' data-toggle='tooltip' title='چت کردن' onclick=('" + item.ConnectionId + "')/></li><li style='padding-left:5px; padding-top:5px;'></li><li class='pull-right' style='padding-right:10px;padding-top:5px;'><img src='/Scripts/Chatroom/Script/users-group.png' style='height:20px;' class='center img-responsive' data-toggle='tooltip' title='کاربران' /></li><li class='pull-right' style='padding-right:10px;padding-top:5px;'><p class='text-center' style='color:white;'>" + item.Username + "</p></li></ul>";
            }
            return uri;
        }
        public string makelistwithpermition(string groupname)
        {
            string uri = "";
            var find = Users.Where(a => a.Group == groupname).ToList();
            foreach (var item in find)
            {
                int count = 0;
                foreach (var permition in permitionlist)
                {
                    if (permition == item.ConnectionId)
                    {
                        count += 1;
                    }
                }
                if (count!=0)
                {
                    char between = '"';
                    uri += "<ul class='list-inline' style='padding-left:30px;'><li style='padding-left:5px; padding-top:5px;'><img src='/Scripts/Chatroom/Script/video-camera.png' style='height:20px;' class='center img-responsive' data-toggle='tooltip' title='چت کردن'   /></li><li style='padding-left:5px; padding-top:5px;'><img src='/Scripts/Chatroom/Script/hold.png' style='height:20px;' class='center img-responsive' data-toggle='tooltip' onclick='permitionaccept(" + between + item.ConnectionId + between + ");' title='اجازه'  /></li><li class='pull-right' style='padding-right:10px;padding-top:5px;'><img src='/Scripts/Chatroom/Script/users-group.png' style='height:20px;' class='center img-responsive' data-toggle='tooltip' title='کاربران' /></li><li class='pull-right' style='padding-right:10px;padding-top:5px;'><p class='text-center' style='color:white;'>" + item.Username + "</p></li></ul>";  
                }
                else
                {
                    uri += "<ul class='list-inline' style='padding-left:30px;'><li style='padding-left:5px; padding-top:5px;'><img src='/Scripts/Chatroom/Script/video-camera.png' style='height:20px;' class='center img-responsive' data-toggle='tooltip' title='چت کردن' onclick=('" + item.ConnectionId + "')/></li><li style='padding-left:5px; padding-top:5px;'></li><li class='pull-right' style='padding-right:10px;padding-top:5px;'><img src='/Scripts/Chatroom/Script/users-group.png' style='height:20px;' class='center img-responsive' data-toggle='tooltip' title='کاربران' /></li><li class='pull-right' style='padding-right:10px;padding-top:5px;'><p class='text-center' style='color:white;'>" + item.Username + "</p></li></ul>";
                }
            }
            return uri;
        }
        public async Task sendmassege(string groupname,string message)
        {
           
            await this.Groups.Add(this.Context.ConnectionId, groupname);
            string name = findnameofuser(this.Context.ConnectionId, groupname);
            if (name!="")
            {
                this.Clients.OthersInGroup(groupname).messagesender(message, name);
            }
            
            
        }

        public async Task deletescreen(string groupname)
        {
            this.Clients.OthersInGroup(groupname).hidedesktop();
        }

        public async Task cancelallejaze(string groupname)
        {
            this.Clients.OthersInGroup(groupname).cancelejaze();
        }
        public void SendPermition(string groupname)
        {
            permitionlist.Add(Context.ConnectionId);
            string lists = makelistwithpermition(groupname);
            this.Clients.OthersInGroup(groupname).additem(lists);
            this.Clients.Caller.additem(lists);
        }

        public void acceptPermitionbyteacher(string connectionId)
        {
            this.Clients.Client(connectionId).clientpermition("معلم اجازه داد شما سوال خود را بپرسید");
            permitionlist.Remove(connectionId);
            var find=Users.FirstOrDefault(a => a.ConnectionId == connectionId);
            if (find!=null)
            {
                 string lists = makelistwithpermition(find.Group);
            this.Clients.OthersInGroup(find.Group).additem(lists);
            this.Clients.Caller.additem(lists);
            }
        }
        public string findnameofuser(string id,string groupname)
        {
            var finduser = Users.FirstOrDefault(a => a.ConnectionId == id && a.Group == groupname);
            if (finduser!=null)
            {
                return finduser.Username;
            }
            return "";
        }
        private static readonly List<User> Users = new List<User>();
        private static readonly List<string> permitionlist = new List<string>();
        private static readonly List<UserCall> UserCalls = new List<UserCall>();
        private static readonly List<CallOffer> CallOffers = new List<CallOffer>();

        public override System.Threading.Tasks.Task OnDisconnected(Boolean flag)
        {
            // Hang up any calls the user is in
            HangUp(); // Gets the user from "Context" which is available in the whole hub

            // Remove the user
            Users.RemoveAll(u => u.ConnectionId == Context.ConnectionId);

            // Send down the new user list to all clients
            SendUserListUpdate("0");

            return base.OnDisconnected(true);
        }

        public void CallUser(string targetConnectionId)
        {
            var callingUser = Users.SingleOrDefault(u => u.ConnectionId == Context.ConnectionId);
            var targetUser = Users.SingleOrDefault(u => u.ConnectionId == targetConnectionId);

            // Make sure the person we are trying to call is still here
            if (targetUser == null)
            {
                // If not, let the caller know
                Clients.Caller.callDeclined(targetConnectionId, "The user you called has left.");
                return;
            }

            // And that they aren't already in a call
            if (GetUserCall(targetUser.ConnectionId) != null)
            {
                Clients.Caller.callDeclined(targetConnectionId, string.Format("{0} is already in a call.", targetUser.Username));
                return;
            }

            // They are here, so tell them someone wants to talk
            Clients.Client(targetConnectionId).incomingCall(callingUser);

            // Create an offer
            CallOffers.Add(new CallOffer
            {
                Caller = callingUser,
                Callee = targetUser
            });
        }

        public void AnswerCall(bool acceptCall, string targetConnectionId)
        {
            var callingUser = Users.SingleOrDefault(u => u.ConnectionId == Context.ConnectionId);
            var targetUser = Users.SingleOrDefault(u => u.ConnectionId == targetConnectionId);

            // This can only happen if the server-side came down and clients were cleared, while the user
            // still held their browser session.
            if (callingUser == null)
            {
                return;
            }

            // Make sure the original caller has not left the page yet
            if (targetUser == null)
            {
                Clients.Caller.callEnded(targetConnectionId, "The other user in your call has left.");
                return;
            }

            // Send a decline message if the callee said no
            if (acceptCall == false)
            {
                Clients.Client(targetConnectionId).callDeclined(callingUser, string.Format("{0} did not accept your call.", callingUser.Username));
                return;
            }

            // Make sure there is still an active offer.  If there isn't, then the other use hung up before the Callee answered.
            var offerCount = CallOffers.RemoveAll(c => c.Callee.ConnectionId == callingUser.ConnectionId
                                                  && c.Caller.ConnectionId == targetUser.ConnectionId);
            if (offerCount < 1)
            {
                Clients.Caller.callEnded(targetConnectionId, string.Format("{0} has already hung up.", targetUser.Username));
                return;
            }

            // And finally... make sure the user hasn't accepted another call already
            if (GetUserCall(targetUser.ConnectionId) != null)
            {
                // And that they aren't already in a call
                Clients.Caller.callDeclined(targetConnectionId, string.Format("{0} chose to accept someone elses call instead of yours :(", targetUser.Username));
                return;
            }

            // Remove all the other offers for the call initiator, in case they have multiple calls out
            CallOffers.RemoveAll(c => c.Caller.ConnectionId == targetUser.ConnectionId);

            // Create a new call to match these folks up
            UserCalls.Add(new UserCall
            {
                Users = new List<User> { callingUser, targetUser }
            });

            // Tell the original caller that the call was accepted
            Clients.Client(targetConnectionId).callAccepted(callingUser);

            // Update the user list, since thes two are now in a call
            SendUserListUpdate("0");
        }

        public void HangUp()
        {
            var callingUser = Users.SingleOrDefault(u => u.ConnectionId == Context.ConnectionId);

            if (callingUser == null)
            {
                return;
            }

            var currentCall = GetUserCall(callingUser.ConnectionId);

            // Send a hang up message to each user in the call, if there is one
            if (currentCall != null)
            {
                foreach (var user in currentCall.Users.Where(u => u.ConnectionId != callingUser.ConnectionId))
                {
                    Clients.Client(user.ConnectionId).callEnded(callingUser.ConnectionId, string.Format("{0} has hung up.", callingUser.Username));
                }

                // Remove the call from the list if there is only one (or none) person left.  This should
                // always trigger now, but will be useful when we implement conferencing.
                currentCall.Users.RemoveAll(u => u.ConnectionId == callingUser.ConnectionId);
                if (currentCall.Users.Count < 2)
                {
                    UserCalls.Remove(currentCall);
                }
            }

            // Remove all offers initiating from the caller
            CallOffers.RemoveAll(c => c.Caller.ConnectionId == callingUser.ConnectionId);

            SendUserListUpdate("0");
        }

        // WebRTC Signal Handler
        public void SendSignal(string signal, string targetConnectionId)
        {
            var callingUser = Users.SingleOrDefault(u => u.ConnectionId == Context.ConnectionId);
            var targetUser = Users.SingleOrDefault(u => u.ConnectionId == targetConnectionId);

            // Make sure both users are valid
            if (callingUser == null || targetUser == null)
            {
                return;
            }

            // Make sure that the person sending the signal is in a call
            var userCall = GetUserCall(callingUser.ConnectionId);

            // ...and that the target is the one they are in a call with
            if (userCall != null && userCall.Users.Exists(u => u.ConnectionId == targetUser.ConnectionId))
            {
                // These folks are in a call together, let's let em talk WebRTC
                Clients.Client(targetConnectionId).receiveSignal(callingUser, signal);
            }
        }

        #region Private Helpers

        private void SendUserListUpdate(string group)
        {
            Users.ForEach(u => u.InCall = (GetUserCall(u.ConnectionId) != null) && u.Group==group);
            Clients.OthersInGroup(group).updateUserList(Users);
        }

        private UserCall GetUserCall(string connectionId)
        {
            var matchingCall =
                UserCalls.SingleOrDefault(uc => uc.Users.SingleOrDefault(u => u.ConnectionId == connectionId) != null);
            return matchingCall;
        }

        #endregion
    }
}