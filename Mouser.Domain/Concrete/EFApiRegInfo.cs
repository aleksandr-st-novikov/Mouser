﻿using Mouser.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mouser.Domain.Concrete
{
    public class EFApiRegInfo : EFAbstract<ApiRegInfo>
    {
        private readonly ApplicationContext _context;
        public EFApiRegInfo(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ApiRegInfo> GetAvailableParnterIdAsync()
        {
            //Отбираю все активные
            var allApiRegInfos = await _context.ApiRegInfos.Where(a => a.IsActive == true).ToListAsync();
            //Отбираю все в работе сегодня
            //var todayApiRegInfos = await _context.ApiSearchSessions.Where(a => a.Date == DateTime.Today).Select(a => a.ApiRegInfo.Id).Distinct().ToListAsync();
            //var todayApiSearchSessions = await _context.ApiSearchSessions.Where(a => a.Date == DateTime.Today && a.IsBusy == true).Include(a => a.ApiRegInfo).ToListAsync();
            //Отбираю кол-во по сегодняшним
            //var sumApiRegInfos = await (from a in _context.ApiSearchSessions
            //                            where a.Date == DateTime.Today
            //                            group a by new { a.Date, a.ApiRegInfo } into g
            //                            select new
            //                            {
            //                                ApiRegInfo = g.Key.ApiRegInfo,
            //                                CountOfRequests = g.Sum(a => a.CountOfRequests)
            //                            }).ToListAsync();
            //Все занятые сегодня
            var busyApiRegInfos = await _context.ApiSearchSessions.Where(a => a.IsBusy == true && a.Date == DateTime.Today).Select(a => a.ApiRegInfo).ToListAsync();
            //Все занятые с количеством
            var busyCountApiRegInfos = await (from p in _context.ApiSearchSessions
                                              where p.IsBusy == true && p.Date == DateTime.Today
                                              group p by new { p.Date, p.ApiRegInfo } into g
                                              select new
                                              {
                                                  ApiRegInfo = g.Key.ApiRegInfo,
                                                  CountApiRegInfo = g.Count()
                                              }).OrderBy(p => p.CountApiRegInfo).FirstOrDefaultAsync();

            foreach (var all in allApiRegInfos)
            {
                if (!busyApiRegInfos.Select(b => b.Id).Contains(all.Id))
                {
                    return all;
                }
            }
            return busyCountApiRegInfos != null ? busyCountApiRegInfos.ApiRegInfo : null;

            //foreach (var all in allApiRegInfos)
            //{
            //    if ((todayApiSearchSessions.FirstOrDefault(a => a.ApiRegInfo.Id == all.Id) == null
            //        && sumApiRegInfos.FirstOrDefault(a => a.ApiRegInfo.Id == all.Id)?.CountOfRequests < 1000)
            //        || !todayApiRegInfos.Contains(all.Id))
            //    {
            //        return all;
            //    }
            //}
            //return null;
        }
    }
}
