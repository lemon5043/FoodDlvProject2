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
    }

}
