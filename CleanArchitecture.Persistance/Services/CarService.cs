using CleanArchitecture.Application.Features.CarFeatures.Command.CreateCar;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Persistance.Services
{
    public class CarService : ICarService
    {
        private readonly AppDbContext _context;

        public CarService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(CreateCarCommand request, CancellationToken cancellationToken)
        {
            Car car = new Car()
            {Name=request.Name,
            Model=request.Model,
            EnginePower=request.EnginePower

            };
            await _context.Set<Car>().AddAsync(car,cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

        }
    }
}
