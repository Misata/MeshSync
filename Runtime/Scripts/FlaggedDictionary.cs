using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.MeshSync {

internal class FlaggedDictionary<K,V> : IEnumerable {
    
    internal void Clear() {
        m_dictionary.Clear();
        m_isDirty = true;
    }

//----------------------------------------------------------------------------------------------------------------------    
    internal void Set(K[] keys, V[] values) {

        m_dictionary.Clear();
        try {
            if (keys != null && values != null && keys.Length == values.Length) {
                int n = keys.Length;
                for (int i = 0; i < n; ++i)
                    m_dictionary[keys[i]] = values[i];
            }

            m_isDirty = false;
        }
        catch (Exception e) {
            Debug.LogError(e);
            m_isDirty = true;
        }

    }

//----------------------------------------------------------------------------------------------------------------------
    internal void Add(K key, V value) {        
        m_dictionary[key] = value;        
        m_isDirty = true;
    }

    internal bool Remove(K key) {
        bool ret = m_dictionary.Remove(key);
        m_isDirty |= ret;
        return ret;
    }
    
//----------------------------------------------------------------------------------------------------------------------    
    internal bool TryGetValue(K key, out V value) {
        return m_dictionary.TryGetValue(key, out value);
    }

    internal Dictionary<K, V>.ValueCollection GetValues() {
         return m_dictionary.Values;
    }
    
    
//----------------------------------------------------------------------------------------------------------------------    
    
    internal void Flush(ref K[] keys, ref V[] values, bool force = false) {
        if (!force && !m_isDirty)
            return;

        m_dictionary.CopyKeyAndValuesTo(ref keys, ref values);
        m_isDirty = false;
    }



//----------------------------------------------------------------------------------------------------------------------
    
    IEnumerator IEnumerable.GetEnumerator()  {
        return m_dictionary.GetEnumerator();
    }
        

//----------------------------------------------------------------------------------------------------------------------    
    
    private Dictionary<K, V> m_dictionary = new Dictionary<K, V>();
    private bool             m_isDirty;
}    
    
} //end namespace