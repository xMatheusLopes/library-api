using System;
using library_api.Interfaces;

namespace library_api.Entities
{
    public class GeneralStatus : IGeneralStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
