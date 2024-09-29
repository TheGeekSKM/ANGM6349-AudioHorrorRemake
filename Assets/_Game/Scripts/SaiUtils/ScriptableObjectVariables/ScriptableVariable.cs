using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SaiUtils.ScriptableVariables
{
    public abstract class ScriptableVariable<T> : ScriptableObject
    {
        [SerializeField] T _value;
        public Action<T> OnValueChanged;

        public T Value
        {
            get => _value;
            set {
                _value = value;
                OnValueChanged?.Invoke(_value);
            }
        }

        [Button]
        public void ChangeValue(T value)
        {
            Value = value;
        }
    }
}
