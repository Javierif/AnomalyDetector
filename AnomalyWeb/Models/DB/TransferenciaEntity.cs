using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnomalyWeb.Models.DB
{
    public class TransferenciaEntity : TableEntity
    {
        public int Money { get; set; }
        public string Time { get; set; }

        public TransferenciaEntity() { }
        public TransferenciaEntity(int money, string time)
        {
            Money = money;
            Time = time;
        }

    }
}
