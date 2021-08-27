using System.Collections.Generic;

namespace Unity.MeshSync {

internal class FlaggedDictionary<K,V> {

    internal bool IsDirty() { return m_isDirty;}

    internal void SetDirty(bool dirty) { return m_isDirty;}
    
    internal void Clear() {
        m_dictionary.Clear();
        m_isDirty = true;
    }
    
    void Flush(ref K[] keys, ref V[] values) {
        if (!m_isDirty)
            return;
        
        keys      = m_dictionary.Keys.ToArray();
        values    = m_dictionary.Values.ToArray();
        m_isDirty = false;
    }
    
    
//----------------------------------------------------------------------------------------------------------------------    
    
    private Dictionary<K, V> m_dictionary = new Dictionary<K, V>();
    private bool             m_isDirty;
}    
    
} //end namespace