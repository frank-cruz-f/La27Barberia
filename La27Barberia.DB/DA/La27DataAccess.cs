namespace La27Barberia.DB.DA
{
    public class La27DataAccess
    {
        protected BarberContext context;

        public La27DataAccess()
        {
            context = new BarberContext();
        }
    }
}