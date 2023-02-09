using System;
using System.Collections.Generic;
using Cinemachine;
using Source.Characters.Train.TrainCar;
using Source.Map.ChunksLoader.ObjectsGeneration;

namespace Source.Characters.Train
{
    internal class CinemachineCameraTargetGroupInit : IObjectsContainer<TrainCar.TrainCar>
    {
        private readonly IObjectsContainer<TrainCar.TrainCar> _objectsContainer;
        private readonly CinemachineTargetGroup _targetGroup;

        public CinemachineCameraTargetGroupInit(IObjectsContainer<TrainCar.TrainCar> objectsContainer,
            CinemachineTargetGroup cinemachineTransposer)
        {
            _targetGroup = cinemachineTransposer ? cinemachineTransposer : throw new ArgumentNullException();
            _objectsContainer = objectsContainer ?? throw new ArgumentNullException();
        }

        public IEnumerable<TrainCar.TrainCar> Objects => _objectsContainer.Objects;
        public int Count => _objectsContainer.Count;

        public void Add(TrainCar.TrainCar tObject)
        {
            AddMember(tObject);
            _objectsContainer.Add(tObject);
        }

        public void Remove(TrainCar.TrainCar tObject)
        {
            RemoveMember(tObject);
            _objectsContainer.Remove(tObject);
        }

        public void Replace(TrainCar.TrainCar replaced, TrainCar.TrainCar newObject)
        {
            _objectsContainer.Replace(replaced, newObject);
            AddMember(newObject);
            RemoveMember(replaced);
        }

        private void AddMember(TrainCar.TrainCar tObject) =>
            _targetGroup.AddMember(tObject.transform, 1, 2);

        private void RemoveMember(TrainCar.TrainCar tObject) =>
            _targetGroup.RemoveMember(tObject.transform);
    }
}
