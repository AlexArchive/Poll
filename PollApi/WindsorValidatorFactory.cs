using Castle.Windsor;
using FluentValidation;
using System;

namespace PollApi
{
    public class WindsorValidatorFactory : ValidatorFactoryBase
    {
        private readonly IWindsorContainer _container; 

        public WindsorValidatorFactory(IWindsorContainer conntainer)
        {
            _container = conntainer;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            if (_container.Kernel.HasComponent(validatorType))
            {
                return _container.Resolve(validatorType) as IValidator;
            }

            return null;
        }
    }
}