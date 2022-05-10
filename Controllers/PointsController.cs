using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ManageRewards.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PointsController : ControllerBase
    {
        [HttpPost]
        public bool AddPoints([FromBody] Transaction trans)
        {
            DBInteractor db = new DBInteractor();
            return db.AddTransaction(trans);
        }

        [HttpGet]
        public Dictionary<string, int> GetPointsBalance()
        {
            DBInteractor db = new DBInteractor();
            return db.GetPointsBalance();
        }

        [HttpGet("Spend/{points}")]
        public Dictionary<string, int> SpendPoints(int points)
        {
            DBInteractor db = new DBInteractor();
            return db.SpendPoints(points);
        }
    }
}
