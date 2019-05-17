namespace EF.DataAccess.DataModel
{
    public class SessionStoreDO
    {
        public int Id { get; set; }
        public SessionDO Session { get; set; }
        public StoreDO Store { get; set; }

    }
}
