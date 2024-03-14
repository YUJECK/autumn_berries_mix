using System;
using System.Collections.Generic;
using autumn_berry_mixВ;
using deKraken.Extensions;

namespace autumn_berries_mix.EC
{
    public sealed class ComponentsMaster
    {
        public readonly Entity Owner;
        
        private readonly Dictionary<Type, Component> _components = new();

        public ComponentsMaster(Entity owner)
            => Owner = owner;

        public Component[] All()
        {
            return _components.ToValueArray();
        }
        
        public TComponent Get<TComponent>()
            where TComponent : Component
        {
            if(_components.ContainsKey(typeof(TComponent)))
                return _components[typeof(TComponent)] as TComponent;

            else
            {
                foreach (var component in _components)
                {
                    if (component.Value is TComponent)
                        return component.Value as TComponent;
                }
            }
            
            return null;
        }

        public TClass FindByBaseClass<TClass>()
            where TClass : class
        {
            foreach (var componentPair in _components)
            {
                if (componentPair.Value is TClass asInterface)
                    return asInterface;
            }

            return null;
        }
        
        public void UpdateAll()
        {
            foreach (var component in _components)
                component.Value.Update();
        }
        
        public void DisposeAll()
        {
            foreach (var component in _components)
                component.Value.Dispose();
        }
        
        public void EnableAll()
        {
            foreach (var component in _components)
                component.Value.Enable();
        }
        
        public void DisableAll()
        {
            foreach (var component in _components)
                component.Value.Disable();
        }
        
        public TComponent Add<TComponent>(TComponent component)
            where TComponent : Component
        {
            if (component == null)
                throw new NullReferenceException("Null component tried to add");
            
            _components.Add(typeof(TComponent), component);
            
            Resolver.Instance().InjectComponent(component);
            
            component.Start(Owner);

            return component;
        }

        public void Remove<TComponent>()
            where TComponent : Component
        {
            if(!_components.Remove(typeof(TComponent)))
               Console.WriteLine("!!!Component wasnt found!!!");
        }

        public void Replace<TComponent>(TComponent component)
            where TComponent : Component
        {
            if (component == null)
                throw new NullReferenceException("Null component tried to replace");

            if (!_components.ContainsKey(typeof(TComponent)))
            {
                Console.WriteLine("!!!Component wasnt found!!!");
                return;
            }
            
            _components[typeof(TComponent)].Dispose();
            
            _components[typeof(TComponent)] = component;
            
            Resolver.Instance().InjectComponent(component);

            component.Start(Owner);
        }
    }
}
