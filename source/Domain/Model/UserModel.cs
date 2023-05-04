using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public sealed record UserModel
    {
        public Guid Id { get; init; }
        public string Name { get; init; }

        public string Email { get; init; }
    }
}
