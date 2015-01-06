using System;
using Castle.Windsor;
using FluentValidation;

namespace PollApi
{
    public class WindsorValidatorFactory : ValidatorFactoryBase
    {
        private readonly IWindsorContainer container; 

        public WindsorValidatorFactory(IWindsorContainer conntainer)
        {
            this.container = conntainer;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            if (container.Kernel.HasComponent(validatorType))
                return container.Resolve(validatorType) as IValidator;
            return null;
        }
    }
}