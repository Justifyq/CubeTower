using System;
using CubeTower.Data;
using CubeTower.Factories;
using CubeTower.Infrastructure.Input;
using CubeTower.UIlts;
using CubeTower.UserInterfaces;
using CubeTower.UserInterfaces.Screens;
using UnityEngine;

namespace CubeTower.Core
{
    public class CubeCatcher :  ICubeCatcher, IDisposable
    {
        
        public CubeCatcher(IInputSystem inputSystem, ICubesFactory factory, ITower tower, Map map, UI ui)
        {
            _inputSystem = inputSystem;
            _factory = factory;
            _tower = tower;
            _map = map;
            _ui = ui;
            
            _inputSystem.OnPress += InputSystem_OnPress; 
            _inputSystem.OnRelease += InputSystem_OnRelease;
        }
        
        public event Action<CubeView> OnCatch;
        public event Action<CubeView> OnRelease;
        
        private readonly IInputSystem _inputSystem;
        private readonly ICubesFactory _factory;
        private readonly ITower _tower;
        private readonly Map _map;
        private readonly UI _ui;
        
        public Vector2 CatchPos { get; private set; }
        public Vector2 ReleasePos { get; private set; }

        public Vector2 ActualPosition => _map.GetWorldPoint(_inputSystem.PointerPos);
        
        public CubeView Cube { get; private set; }
        

        public void Dispose()
        {
            _inputSystem.OnPress -= InputSystem_OnPress; 
            _inputSystem.OnRelease -= InputSystem_OnRelease;
        }
        
        public void Catch(CubeView cube)
        {
            Cube = cube;
            
            cube.gameObject.SetActive(cube.Data.Placed);

            if (cube.Data.Placed)
                cube.SetSelected(true);
            else
                cube.gameObject.SetActive(false);

            CatchPos = _map.GetWorldPoint(_inputSystem.PointerPos);
            
            OnCatch?.Invoke(Cube);
        }
        
        public void Catch(Cube cube, Vector2 pos)
        {
            if (!_tower.IsFreeSpaceForCube())
            {
                _ui.Get<PopupScreenUI>().Assert(Glossary.CantBuild);
                return;
            }
            
            CatchPos = _map.GetWorldPoint(_inputSystem.PointerPos);
            Catch(_factory.SpawnCube(cube, CatchPos));
        }
        
        private void InputSystem_OnRelease(Vector2 pos)
        {
            if (Cube == null) 
                return;

            Release();
        }

        private void Release()
        {
            ReleasePos = _map.GetWorldPoint(_inputSystem.PointerPos);
            
            OnRelease?.Invoke(Cube);
            
            if (Cube.Data.Placed)
                Cube.SetSelected(false);
            
            Cube = null;
        }

        private void InputSystem_OnPress(Vector2 pos)
        {
            Vector2 point = _map.GetWorldPoint(pos);
            
            if (_map.Ground >= point.y)
                return;
            
            CubeView cube = _tower.FindCube(point);
            
            if (cube != null)
                Catch(cube);
        }

        ~CubeCatcher() => Dispose();
    }
}