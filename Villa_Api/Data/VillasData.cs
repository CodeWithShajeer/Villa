using Villa_Api.DTOs;

namespace Villa_Api.Data
{
    public static class VillasData
    {
        public static  List<VillaDto> VillaList=new List<VillaDto>
            {
                new VillaDto { Id = 1, Name = "villa1",Occupency=3,Sqft=400 },
                new VillaDto { Id = 2, Name = "villa2",Occupency=2,Sqft=500 },
            };

    }
}
