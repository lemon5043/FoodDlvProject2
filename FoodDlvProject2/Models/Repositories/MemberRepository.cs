﻿using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;
using FoodDlvProject2.Models.Infrastructures.ExtensionMethods;
using Microsoft.EntityFrameworkCore;

namespace FoodDlvProject2.Models.Repositories
{
	public class MemberRepository
	{
		private readonly AppDbContext db;

		public MemberRepository(AppDbContext db)
		{
			this.db = db;
		}
		public IEnumerable<MemberDTO> GetMembers()
		{
			var query = db.Members.Select(x => new MemberDTO
			{
				Id = x.Id,
				LastName = x.LastName,
				FirstName = x.FirstName,
				Gender = x.Gender,
				Account = x.Account,
				RegistrationTime = x.RegistrationTime,
			});
			return query.Select(x => x.ToEntity());
		}
		public MemberDTO GetOnly(int? id)
		{
			if (db.Members == null) throw new Exception("找不到資料，請再試一次");

			var query = db.Members.Select(x => new MemberDTO
			{
				Id = x.Id,
				AccountStatusId = x.AccountStatusId,
				FirstName = x.FirstName,
				LastName = x.LastName,
				Phone=x.Phone,
				Gender = x.Gender,
				Birthday = x.Birthday,
				Email = x.Email,
				Balance = x.Balance,
				Account = x.Account,
				Password = x.Password,
				RegistrationTime = x.RegistrationTime,

			}).FirstOrDefault(m => m.Id == id);

			if (query == null) throw new Exception("找不到資料");

			return query;
		}
		public void Edit(MemberEditDTO model)
		{
			try
			{
				var EFModel = ToEFModle(model);
				db.Attach(EFModel);
				string[] updateModel = { "FirstName","LastName",  "Gender", "Birthday", "Phone", "Email",
					"Account", "Password" };

				foreach (var property in updateModel)
				{
					db.Entry(EFModel).Property(property).IsModified = true;
				}

				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!MemberExists(model.Id)) throw new Exception("已經有相同的資料，請重新修改。");
			}
		}

		private bool MemberExists(int id)
		{
			return db.Members.Any(m => m.Id == id);
		}
	}
}