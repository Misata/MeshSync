using System.Collections.Generic;

namespace Unity.MeshSync {

internal class FlaggedDictionary<K,V> {

    internal bool IsDirty() { return m_isDirty;}
    
//----------------------------------------------------------------------------------------------------------------------    
    
    private Dictionary<K, V> m_dictionary;
    private bool             m_isDirty;
}    
    
} //end namespace