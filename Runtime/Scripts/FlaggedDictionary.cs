using System.Collections.Generic;

namespace Unity.MeshSync {

internal class FlaggedDictionary<K,V> {

    internal bool IsDirty() { return m_isDirty;}

    internal void SetDirty(bool dirty) { m_isDirty = dirty;}
    
    internal void Clear() {
        m_dictionary.Clear();
        m_isDirty = true;
    }
    
    void Flush(ref K[] keys, ref V[] values) {
        if (!m_isDirty)
            return;

        m_dictionary.ToKeyAndValues(ref keys, ref values);
        m_isDirty = false;
    }
    
    
//----------------------------------------------------------------------------------------------------------------------    
    
    private Dictionary<K, V> m_dictionary = new Dictionary<K, V>();
    private bool             m_isDirty;
}    
    
} //end namespace