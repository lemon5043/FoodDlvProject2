using FoodDlvAPI.Models;
using FoodDlvAPI.Models.DTOs;
using FoodDlvAPI.Models.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDlvAPI.Models.Repositories
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private AppDbContext db;

        public DeliveryRepository(AppDbContext db)
        {
            this.db = db;
        }

        //外送員切換工作狀態
        public async void ChangeWorkingStatus(int dirverId)
        {
            if (db.DeliveryDrivers == null) throw new Exception("抱歉，找不到指定資料，請確認後再試一次");

            var query = await db.DeliveryDrivers
                .Where(x => x.Id == dirverId)
                .Select(x => new DeliveryDriver
                {
                    Id = x.Id,
                    WorkStatuseId = x.WorkStatuseId,
                    AccountStatusId = x.AccountStatusId
                })
                .FirstOrDefaultAsync();

            if (query == null) throw new Exception("您的帳號處於未開通或是禁止使用狀態，請聯絡客服了解詳細情況");

            //上線
            if (query.WorkStatuseId < 3)
            {
                int AccountAvailableId = 2;
                if (query.AccountStatusId != AccountAvailableId) throw new Exception("您的帳號處於未開通或是禁止使用狀態，請聯絡客服了解詳細情況");

                int onlineWorkStatusId = 3;
                query.WorkStatuseId = onlineWorkStatusId;

                string updateModel = "WorkStatuseId" ;
                db.Attach(query);

                db.Entry(query).Property(updateModel).IsModified = true;

                db.SaveChanges();
                return;
            }

            //下線
            if (query.WorkStatuseId == 3)
            {
                int offlineWorkStatusId = 2;

                query.WorkStatuseId = offlineWorkStatusId;

                string updateModel =  "WorkStatuseId" ;
                db.Attach(query);

                db.Entry(query).Property(updateModel).IsModified = true;

                db.SaveChanges();
                return;
            }
            throw new Exception("抱歉，找不到指定資料，請確認後再試一次");
        }

        public async Task<AasignmentOrderDTO> GetOrderDetail(int orderId)
        {
            if (db.Orders == null) throw new Exception("抱歉，找不到指定資料，請確認後再試一次");

            var query = await db.Orders
                .Where(x => x.Id == orderId)
                .Select(x => new AasignmentOrderDTO
                {
                    OrderId = x.Id,
                    StoreAddress = x.Store.Address,
                })
                .FirstOrDefaultAsync();

            if (query == null) throw new Exception("抱歉，找不到指定資料，請確認後再試一次");

            return query;
        }


        
        //傳送店家資料
        public Task<AasignmentOrderDTO> NavigationToStore(int orderId)
        {
            if (db.Orders == null) throw new Exception("抱歉，找不到指定資料，請確認後再試一次");

            var query = db.Orders
                .Where(x => x.Id == orderId)
                .Select(x => new AasignmentOrderDTO
                {
                    OrderId = x.Id,
                    StoreAddress = x.Store.Address,
                    StoreName = x.Store.StoreName,
                    //todo OrderDetails= x.OrderDetails
                }).FirstOrDefaultAsync();


            if (query == null) throw new Exception("抱歉，找不到指定資料，請確認後再試一次");

            return query;
        }


        public async Task<AasignmentOrderDTO> NavigationToCustomer(int orderId)
        {
            if (db.Orders == null) throw new Exception("抱歉，找不到指定資料，請確認後再試一次");

            var query = await db.Orders
                .Where(x => x.Id == orderId)
                .Select(x => new AasignmentOrderDTO
                {
                    StoreAddress = x.Store.Address,
                    DeliveryAddress = x.DeliveryAddress,
                }).FirstOrDefaultAsync();

            if (query == null) throw new Exception("抱歉，找不到指定資料，請確認後再試一次");

            return query;
        }

        //紀錄外送狀態
        public async Task MarkOrderStatus(int orderId)
        {
            if (db.OrderSchedules == null) throw new Exception("抱歉，找不到指定資料，請確認後再試一次");

            var query = await db.OrderSchedules
                .Where(x => x.OrderId == orderId)
                .Select(x => new OrderSchedule
                {
                    OrderId = x.OrderId,
                    StatusId = x.StatusId,
                }).LastOrDefaultAsync();

            if (query == null) throw new Exception("抱歉，找不到指定資料，請確認後再試一次");
            if (query.StatusId < 3 || query.StatusId > 5) throw new Exception("抱歉，指定為不可外送狀態，請重新確認訂單狀態");

            query.StatusId++;
            query.MarkTime= DateTime.UtcNow;

            db.Add(query);
            db.SaveChanges();
        }

        //外送狀態切換
        public async void ChangeDeliveryStatus(int dirverId)
        {
            if (db.DeliveryDrivers == null) throw new Exception("抱歉，找不到指定資料，請確認後再試一次");

            var query = await db.DeliveryDrivers
                .Where(x => x.Id == dirverId)
                .Select(x => new DeliveryDriver
                {
                    Id = x.Id,
                    WorkStatuseId = x.WorkStatuseId,
                })
                .FirstOrDefaultAsync();

            if (query == null) throw new Exception("您的帳號處於未開通或是禁止使用狀態，請聯絡客服了解詳細情況");

            //切換為外送中
            if (query.WorkStatuseId == 3)
            {
                int DeliveringId = 4;
                query.WorkStatuseId = DeliveringId;

                string updateModel =  "WorkStatuseId";
                db.Attach(query);

                db.Entry(query).Property(updateModel).IsModified = true;

                db.SaveChanges();
                return;
            }

            //切換為等待接單
            if (query.WorkStatuseId == 4)
            {
                int onlineWorkStatusId = 3;

                query.WorkStatuseId = onlineWorkStatusId;

                string updateModel =  "WorkStatuseId" ;
                db.Attach(query);

                db.Entry(query).Property(updateModel).IsModified = true;

                db.SaveChanges();
                return;
            }
            throw new Exception("抱歉，找不到指定資料，請確認後再試一次");
        }
    }
}