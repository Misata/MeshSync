using System;
using System.Collections.Generic;

namespace Unity.MeshSync  {
[Serializable]
internal static class DictionaryExtensions {

    internal static void CopyKeyAndValuesTo<K, V>(this Dictionary<K, V> dic, ref K[] keys, ref V[] values) {
        int count = dic.Count;
        keys   = new K[count];
        values = new V[count];        
        dic.Keys.CopyTo(keys,0);
        dic.Values.CopyTo(values,0);
    }
    
}

} //end namespace
