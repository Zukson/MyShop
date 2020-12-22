using AutoMapper;
using MyShop.API.Data;
using MyShop.API.Domain;
using MyShop.API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.API.Services
{
    public class TagService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public TagService(DataContext dataContext,IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }
        public async Task<bool> CreateTagAsync(Tag tag)
        {
           await _dataContext.Tags.AddAsync(_mapper.Map<TagDTO>(tag));

            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        //public async Task<bool> CheckIfTagExist(Tag tag)
        //{
        //var tagFound=   await  _dataContext.Tags.FindAsync(tag);
        //    if (tagFound is null)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }

        //}
           
    }
}
