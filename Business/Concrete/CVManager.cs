using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.FileOperations;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using Entities.DTOs;
using Entities.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CVManager : ICVService
    {
        private readonly ICVDal _cVDal;

        public CVManager(ICVDal cVDal)
        {
            _cVDal = cVDal;
        }

        public async Task<IResult> Add(CVDto cVDto)
        {
            IResult result = BusinessRules.Run(CheckIfFileExtensionValid(cVDto.file.FileName));
            if (result!=null)
            {
                return result;
            }
            string path =await FileOperationsHelper.UploadSingleAsync("images", cVDto.file);

            CV cV=new CV();
            cV.Id = cVDto.Id;
            cV.CandidateId = cVDto.CandidateId;
            cV.Photo = path;
            cV.Description=cVDto.Description;
            cV.IsActive=true;

            _cVDal.Add(cV);
            return new SuccessResult(Messages.CVAdded);
        }

        public IResult Delete(int id)
        {
            var result = _cVDal.Get(x => x.Id == id);
            _cVDal.Delete(result);
            return new SuccessResult(Messages.CVDeleted);
        }

        public IDataResult<List<CV>> GetByCandidateId(int candidateId)
        {
            return new SuccessDataResult<List<CV>>(_cVDal.GetAllByInclude(x=>x.CandidateId==candidateId,
                include: c => c.Include(x => x.Universities)
                .Include(x=>x.JobExperiences)
                .Include(x=>x.Technologies)));
        }

        public IDataResult<CV> GetById(int id)
        {
            return new SuccessDataResult<CV>(_cVDal.Get(x=>x.Id==id));
        }

        public async Task<IResult> Update(CVDto cVDto)
        {
            var result=_cVDal.Get(x=>x.Id == cVDto.Id);
            string newPath=null;
            if (cVDto.file!=null)
            {
                string direk=Directory.GetCurrentDirectory();
                string path = direk + @"\wwwroot\" + result.Photo;
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                newPath = await FileOperationsHelper.UploadSingleAsync("images", cVDto.file);
            }else
            {
                newPath = result.Photo;
            }

            CV cV = new CV();
            cV.Id = cVDto.Id;
            cV.CandidateId = cVDto.CandidateId;
            cV.Photo = newPath;
            cV.Description = cVDto.Description;
            cV.IsActive = true;

            _cVDal.Update(cV);
            return new SuccessResult(Messages.CVUpdated);
        }

        


        /*---Busines Rule---*/

        private IResult CheckIfFileExtensionValid(string file)
        {
            if (!Regex.IsMatch(file, @"([A-Za-z0-9\-]+)\.(png|PNG|jpg|jpeg|JPG)"))
            {
                return new ErrorResult(Messages.InvalidFileExtension);
            }
            return new SuccessResult();
        }

        
    }
}
