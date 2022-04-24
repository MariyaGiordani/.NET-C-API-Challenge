using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIChallenge.Models
{
    public class Security
    {
        public byte[] SaltPassword { get; set; }
        public int Id { get; set; }
        public User User { get; set; }
    }
}
