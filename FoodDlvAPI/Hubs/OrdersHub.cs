using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace FoodDlvAPI.Hubs
{
    public class OrderHub : Hub
    {

        /// <summary>
        /// 將使用者加入Hub群組，方便建立連線
        /// </summary>
        /// <param name="id">自身Id e.g.storeId、MemberId</param>
        /// <param name="role">使用者角色Id e.g.Store、Member ，不分大小寫</param>
        /// <returns></returns>
        public async Task JoinGroup(int id,string role)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, role.ToLower() + id.ToString());
        }

        /// <summary>
        /// 將使用者離開Hub群組
        /// </summary>
        /// <param name="id">自身Id e.g.storeId、MemberId</param>
        /// <param name="role">使用者角色Id e.g.Store、Member ，不分大小寫</param>
         /// <returns></returns>
        public async Task LeaveGroup(int id, string role)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, role.ToLower() + id.ToString());
        }

        /// <summary>
        /// 會員=>商家
        /// 會員生成訂單，向商家傳送訂單
        /// </summary>
        /// <param name="StordId">商家Id</param>
        /// <param name="orderId">訂單Id</param>
        /// <returns></returns>
        public async Task NewOrder(int StordId,int orderId)
        {
            string groupName = "Store" + StordId.ToString();
            await Clients.Group(groupName).SendAsync("NewOrder", orderId);
        }

        /// <summary>
        /// 商家=>外送員
        /// 商家完成訂單，向外送員傳遞請求
        /// </summary>
        /// <param name="DriverId">外送員Id</param>
        /// <param name="orderId">訂單Id</param>
        /// <returns></returns>
        public async Task AssignOrder(int DriverId, int orderId)
        {
            string groupName = "Driver" + DriverId.ToString();
            await Clients.Group(groupName).SendAsync("AssignOrder", orderId);
        }

        /// <summary>
        /// 外送員=>商家
        /// 外送員拒絕接單，重新指派請求
        /// </summary>
        /// <param name="StordId">商家Id</param>
        /// <param name="orderId">訂單Id</param>
        /// <returns></returns>
        public async Task DeclineOrder(int StordId, int orderId)
        {
            string groupName = "Store" + StordId.ToString();
            await Clients.Group(groupName).SendAsync("DeclineOrder", orderId);
        }

        /// <summary>
        /// 外送員=>會員
        /// 外送員送達訂單，向會員發出通知
        /// </summary>
        /// <param name="MemberId">會員Id</param>
        /// <param name="orderId">訂單Id</param>
        /// <returns></returns>
        public async Task OrderArrive(int MemberId, int orderId)
        {
            string groupName = "Member" + MemberId.ToString();
            await Clients.Group(groupName).SendAsync("OrderArrive", orderId);
        }
    }

}
