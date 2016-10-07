using AutoMapper.Attributes;

namespace oneWeekHackathon.ViewModel
{
    public static class ModelMapper
    {
        public static void Init()
        {
            typeof(ModelMapper).Assembly.MapTypes();
        }
    }
}
