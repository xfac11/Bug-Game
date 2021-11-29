using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallFastIk : MonoBehaviour
{
    private DitzelGames.FastIK.FastIKFabric[] _allIk;
    private int _index = 0;
    // Start is called before the first frame update
    void Start()
    {
        _allIk = FindObjectsOfType<DitzelGames.FastIK.FastIKFabric>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        int length = _allIk.Length / 8;
        for (int i = 0; i < length; i++)
        {
            ResolveOne();
        }
    }

    private void ResolveOne()
    {
        if (_index == _allIk.Length)
        {
            _index = 0;
        }
        _allIk[_index].ResolveIK();
        _index++;
    }
}
