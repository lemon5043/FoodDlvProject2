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

                db.Update(query);
                db.SaveChanges();
            }

            //下線
            if (query.AccountStatusId == 3)
            {
                int offlineWorkStatusId = 2;

                query.WorkStatuseId = offlineWorkStatusId;

                db.Update(query);
                db.SaveChanges();
            }
            throw new Exception("抱歉，找不到指定資料，請確認後再試一次");
        }

        //public async void ChangeToOffline(int dirverId)
        //{
        //    if (db.DeliveryDrivers == null) throw new Exception("抱歉，找不到指定資料，請確認後再試一次");

        //    int onlineWorkStatusId = 3;

        //    var query = await db.DeliveryDrivers
        //        .Where(x => x.Id == dirverId)
        //        .Select(x => new DeliveryDriver
        //        {
        //            Id = x.Id,
        //            WorkStatuseId = x.WorkStatuseId,
        //        })
        //        .FirstOrDefaultAsync();

        //    if (query == null) throw new Exception("抱歉，找不到指定資料，請確認後再試一次");

        //    int offlineWorkStatusId = 2;

        //    query.WorkStatuseId = onlineWorkStatusId;

        //    db.Update(query);

        //    db.SaveChanges();
        //}

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

        public async Task<AasignmentOrderDTO> NavigationToCustomer(int orderId)
        {
            if (db.Orders == null) throw new Exception("抱歉，找不到指定資料，請確認後再試一次");

            var query = await db.Orders
                .Where(x => x.Id == orderId)
                .Select(x => new AasignmentOrderDTO
                {
                    StoreAddress = x.Store.Address,
                    DeliveryAddress = x.DeliveryAddress,
                })
                .FirstOrDefaultAsync();
            if (query == null) throw new Exception("抱歉，找不到指定資料，請確認後再試一次");

            return query;
        }

        public void NavigationToStore(int orderId)
        {
            throw new NotImplementedException();
        }

        public void OrderArrive(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}