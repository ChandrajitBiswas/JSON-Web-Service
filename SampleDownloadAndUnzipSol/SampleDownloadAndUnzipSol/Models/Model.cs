using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleDownloadAndUnzipSol.Models
{
    public class GenderPetList
    {
        public List<GenderPet> GenderPets { get; set; }
    }

    public class Owner
    {
        public string Gender { get; set; }
        public List<Pet> Pets { get; set; }
    }

    public class GenderPet
    {
        public string OwnerGender { get; set; }
        public string PetType { get; set; }
        public string PetName { get; set; }
    }

    public class Pet
    {
        public string Type { get; set; }
        public string Name { get; set; }
    }
}