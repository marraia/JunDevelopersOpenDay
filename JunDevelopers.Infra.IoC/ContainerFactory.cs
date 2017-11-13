﻿using SimpleInjector;

namespace JunDevelopers.Infra.IoC
{
    public static class ContainerFactory
    {
        public static Container Container => GetContainerInstance();

        private static Container _container;
        private static Container GetContainerInstance()
        {
            if (_container == null)
            {
                _container = new Container();

            }

            return _container;
        }
    }
}
