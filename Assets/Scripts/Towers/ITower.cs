using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITower
{
    public void LevelUp();
    //public void AddArtifact(IArtifact artifact);
    //public List<IArtifact> GetArtifact();
    Transform transform { get; }
    string name { get; }
    public int Level { get; }

    //public List<IArtifact> Artifacts { get; }
}
