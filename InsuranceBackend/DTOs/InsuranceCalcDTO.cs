namespace InsuranceBackend.DTOs
{
    public class InsuranceCalcDTO
    {
        public string Name{ get; set; }
        public int Age{ get; set; }
        public DateTime DOB { get; set; }
        public int OccupationID { get; set; }
        public float DeathSumInsured { get; set; }
    }
}
