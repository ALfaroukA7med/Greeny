using Greeny.BLL.ModelVM.ReferencePlanet;


public class RefPlanet :Profile
    {
            public RefPlanet()
            {
                CreateMap<CreateRefPlanetVM, ReferencePlanet>();

                CreateMap<UpdateRefPlanetVM, ReferencePlanet>();

                CreateMap<ReferencePlanet, DetailsRefPlanetVM>();

                CreateMap<ReferencePlanet, DetailsForDashboard>();

            }
     }
    
