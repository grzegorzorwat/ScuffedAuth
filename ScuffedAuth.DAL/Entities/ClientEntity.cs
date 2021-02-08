namespace ScuffedAuth.DAL.Entities
{
    public class ClientEntity
    {
        public string Id { get; set; }

        public string Secret { get; set; }

        public string RedirectUri { get; set; }
    }
}
