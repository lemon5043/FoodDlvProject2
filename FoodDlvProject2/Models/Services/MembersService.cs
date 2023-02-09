using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;
using FoodDlvProject2.Models.Repositories;
using FoodDlvProject2.Models.Services.Interfaces;
using NuGet.Protocol.Core.Types;

namespace FoodDlvProject2.Models.Services
{
	public class MembersService
	{
		private readonly IMemberRepository _repository;
		
		public MembersService(IMemberRepository repository)
		{
			_repository = repository;
		}
        public IEnumerable<MemberDTO> GetMembers()
           => _repository.GetMembers();
        public MemberDTO GetOnly(int? id)
		{
			return _repository.GetOnly(id);
		}

		public void Edit(MemberEditDTO model)
		{
		
			_repository.Edit(model);
		}

	
	}
}

