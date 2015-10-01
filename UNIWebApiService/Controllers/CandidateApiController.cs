using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using UNIEntity;

namespace UNIWebApiService.Controllers
{
    public class CandidateApiController : ApiController
    {

        // GET api/candidateapi
        [ActionName("GetPageData")]
        public ResultResponse<CandidatePageRequired> GetPageData()
        {
            var response = new ResultResponse<CandidatePageRequired>();

            try
            {
                var unidb = new UNIDBContext();
                var queryable = unidb.Set<CandidateDetail>();

                response.Object = new CandidatePageRequired()
                {
                    Links = queryable.Select(x => x.Link).Distinct().Where(x => x != string.Empty).OrderBy(x => x).ToList(),
                    CurrentLocations = queryable.Select(x => x.CurrentLocation).Distinct().Where(x => x != string.Empty).OrderBy(x => x).ToList(),
                    PreferedLocations = queryable.Select(x => x.PreferedLocation).Distinct().Where(x => x != string.Empty).OrderBy(x => x).ToList(),
                    Qualifications = queryable.Select(x => x.Qualification).Distinct().Where(x => x != string.Empty).OrderBy(x => x).ToList(),
                    AdditionalQualifications =
                        queryable.Select(x => x.AdditionalQualification).Distinct().Where(x => x != string.Empty).OrderBy(x => x).ToList(),
                    JobTitles = queryable.Select(x => x.JobTitle).Distinct().Where(x => x != string.Empty).OrderBy(x => x).ToList(),
                    CurrentIndustries = queryable.Select(x => x.CurrentIndustry).Distinct().Where(x => x != string.Empty).OrderBy(x => x).ToList(),
                    PreferredIndustries = queryable.Select(x => x.PreferredIndustry).Distinct().Where(x => x != string.Empty).OrderBy(x => x).ToList(),
                    NoticePeriods = queryable.Select(x => x.NoticePeriod).Distinct().Where(x => x != string.Empty).OrderBy(x => x).ToList(),
                };

                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.Exception = ex;
                response.IsSuccess = false;
            }

            return response;
        }

        // GET api/candidateapi/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/candidateapi
        [ActionName("Get")]
        public ResultResponse<CandidateDetail> PostGet(CandidateDetailSearchRequest request)
        {
            var response = new ResultResponse<CandidateDetail>();

            try
            {
                var unidb = new UNIDBContext();
                var queryable = unidb.Set<CandidateDetail>()
                    .Where(x => x.Name.Contains(request.Name)
                                && x.Qualification.Contains(request.Qualification)
                                && (string.IsNullOrEmpty(request.Gender) || (x.Gender == request.Gender))
                                && (string.IsNullOrEmpty(request.MarritalStatus) || (x.MarritalStatus == request.MarritalStatus))
                                &&
                                (!request.SearchByProcessedDate ||
                                 (x.ProcessedDate >= request.FromProcessedDate &&  x.ProcessedDate <= request.ToProcessedDate)
                                    ))
                    .OrderBy(x => x.SrNo);

                var skipRecord = ((request.PageNo > 0) ? (request.PageNo - 1) * request.PageSize : 0);

                response.PageData = queryable.Skip(skipRecord).Take(request.PageSize).ToList();
                response.TotalItem = queryable.Count();
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Exception = ex;
            }

            return response;
        }

        // POST api/candidateapi
        [ActionName("Save")]
        public ResultResponse<CandidateDetail> PostSave(CandidateDetail request)
        {
            var response = new ResultResponse<CandidateDetail>();

            try
            {
                var unidb = new UNIDBContext();
                if (request.SrNo < 1)
                {
                    //var maxId = unidb.CandidateDetails.Any() ? unidb.CandidateDetails.Select(x => x.SrNo).Max() : 0;
                    //request.SrNo = maxId + 1;
                    unidb.CandidateDetails.Add(request);
                }
                else
                {
                    unidb.CandidateDetails.Attach(request);
                    var entry = unidb.ChangeTracker.Entries<CandidateDetail>().FirstOrDefault(e => e.Entity == request);

                    if (entry != null)
                        entry.State = EntityState.Modified;
                }
                unidb.SaveChanges();
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Exception = ex;
            }

            return response;
        }

        // PUT api/candidateapi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/candidateapi/5
        public void Delete(int id)
        {
        }
    }
}
