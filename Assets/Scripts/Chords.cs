using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Chord 
{
    
    public Chord(ChordEnum _chord, MusicalKey _key, ChordFunction _function, List<Note> _notes ){
        this.chord = _chord;
        this.keyThisChordIsIn = _key;
        this.functionWithinKey = _function;
        this.notes = _notes;
        
    }
    public Chord(Chord _chord)
    {
        this.chord = _chord.chord;
        this.keyThisChordIsIn = _chord.keyThisChordIsIn;
        this.functionWithinKey = _chord.functionWithinKey;
        this.notes = _chord.notes;

    }
    public ChordEnum chord = ChordEnum.none;
    public MusicalKey keyThisChordIsIn = MusicalKey.none; 
    public ChordFunction functionWithinKey = ChordFunction.none;
    public List<Note> notes = null;
}


public class Chords : MonoBehaviour
{
    
    

    Dictionary<ChordEnum, List<Note>> allChords = new Dictionary<ChordEnum, List<Note>>();

    Dictionary<MusicalKey,  Dictionary<ChordFunction, List<ChordEnum> >  > functionalChords = new Dictionary<MusicalKey, Dictionary<ChordFunction, List<ChordEnum>>>();

    //Dictionary<FunctionalCMajor, Dictionary<ChordEnum, List<Note>>> cMajorChords = new Dictionary<FunctionalCMajor, Dictionary<ChordEnum, List<Note>>>();

    const int keyOffset = 48;  // 49 or 48 bug here

    public int GetKeyOffset() { return keyOffset ;}

    public Chord GetChordOfType(MusicalKey key, ChordFunction function) // returns random chord in a given key that performs given function, (functional harmony)
    {
        Chord chord;
        ChordEnum chordname = ChordEnum.none;

        if (functionalChords[key].ContainsKey(function)) // this should always pass, just to be safe
        {
            chordname = functionalChords[key][function][Random.Range((int)0, functionalChords[key][function].Count-1)]; // get a random chord of this function
        }
        else
        {
            Debug.LogError("Functional type not recognised");
        }


        if (allChords.ContainsKey(chordname)) // just in case
        {
            chord = new Chord(chordname, key, function, allChords[chordname]);
            return chord;
        }
        else
        {
            Debug.LogError("chord does not exist");
            return null;
        }
        
             
    }

    private void Start()
    {

        // C1 Major
        allChords[ChordEnum.C1] = new List<Note> { Note.c1, Note.e1, Note.g1 };
        allChords[ChordEnum.Dm1] = new List<Note> { Note.d1, Note.f2, Note.a2 };
        allChords[ChordEnum.Em1] = new List<Note> { Note.e1, Note.g1, Note.b2 };
        allChords[ChordEnum.F1] = new List<Note> { Note.f1, Note.a2, Note.c2 };
        allChords[ChordEnum.G1] = new List<Note> { Note.g1, Note.b2, Note.d2 };

        // C2 Major
        allChords[ChordEnum.Am2] = new List<Note> { Note.a2, Note.c2, Note.e2 };
        allChords[ChordEnum.Bdim2] = new List<Note> { Note.b2, Note.d2, Note.f2 };
        allChords[ChordEnum.C2] = new List<Note> { Note.c2, Note.e2, Note.g2 };
        allChords[ChordEnum.Dm2] = new List<Note> { Note.d2, Note.f2, Note.a3 };
        allChords[ChordEnum.Em2] = new List<Note> { Note.e2, Note.g2, Note.b3 };
        allChords[ChordEnum.F2] = new List<Note> { Note.f2, Note.a3, Note.c3 };
        allChords[ChordEnum.G2] = new List<Note> { Note.g2, Note.b3, Note.d3 };

        // C3 Major
        allChords[ChordEnum.Am3] = new List<Note> { Note.a3, Note.c3, Note.e3 };
        allChords[ChordEnum.Bdim3] = new List<Note> { Note.b3, Note.d3, Note.f3 };
        allChords[ChordEnum.C3] = new List<Note> { Note.c3, Note.e3, Note.g3 };
        //allChords[ChordEnum.Dm3] = new List<Note> { Note.d3, Note.f3, Note.a4 };
        //allChords[ChordEnum.Em3] = new List<Note> { Note.e3, Note.g3, Note.b4 };
        //allChords[ChordEnum.F3] = new List<Note> { Note.f3, Note.a4, Note.c4 };
        //allChords[ChordEnum.G3] = new List<Note> { Note.g3, Note.b4, Note.d4 };

        //// C4 Major
        //allChords[ChordEnum.Am4] = new List<Note> { Note.a4, Note.c4, Note.e4 };
        //allChords[ChordEnum.Bdim4] = new List<Note> { Note.b4, Note.d4, Note.f4 };
        //allChords[ChordEnum.C4] = new List<Note> { Note.c4, Note.e4, Note.g4 };

        // C Major functional chords
        functionalChords[MusicalKey.CMajor] = new Dictionary<ChordFunction, List<ChordEnum>>();
        functionalChords[MusicalKey.CMajor][ChordFunction.root] = new List<ChordEnum> {  ChordEnum.C1, ChordEnum.C2, ChordEnum.C3,};

        functionalChords[MusicalKey.CMajor][ChordFunction.tonic] = new List<ChordEnum> {
            (ChordEnum)FunctionalCMajor.I1,       (ChordEnum)FunctionalCMajor.I2,     (ChordEnum)FunctionalCMajor.I3,
            (ChordEnum)FunctionalCMajor.iii1,   (ChordEnum)FunctionalCMajor.iii2,      
            (ChordEnum)FunctionalCMajor.vi1,     (ChordEnum)FunctionalCMajor.vi2,    
        };
        
        functionalChords[MusicalKey.CMajor][ChordFunction.subdominant] = new List<ChordEnum> {
            (ChordEnum)FunctionalCMajor.IV1,       (ChordEnum)FunctionalCMajor.IV2,        
            (ChordEnum)FunctionalCMajor.ii1,       (ChordEnum)FunctionalCMajor.ii2,        
        }; 
        
        functionalChords[MusicalKey.CMajor][ChordFunction.dominant] = new List<ChordEnum> {
                  (ChordEnum)FunctionalCMajor.V1,             (ChordEnum)FunctionalCMajor.V2,     
            (ChordEnum)FunctionalCMajor.viiidim1,       (ChordEnum)FunctionalCMajor.viiidim2,       
        };


    }



}


public enum MusicalKey
{
    none = 0,

    CMajor,
}

public enum ChordFunction
{
    none = 0,

    root,

    tonic,
    dominant,
    subdominant,
}

public enum FunctionalCMajor
{
    none = 0,

    //C1
    I1 = ChordEnum.C1,
    ii1 = ChordEnum.Dm1,
    iii1 = ChordEnum.Em1,
    IV1 = ChordEnum.F1,
    V1 = ChordEnum.G1,
    vi1 = ChordEnum.Am2,
    viiidim1 = ChordEnum.Bdim2,

    //C1
    I2 = ChordEnum.C2,
    ii2 = ChordEnum.Dm2,
    iii2 = ChordEnum.Em2,
    IV2 = ChordEnum.F2,
    V2 = ChordEnum.G2,
    vi2 = ChordEnum.Am3,
    viiidim2 = ChordEnum.Bdim3,

    //C3
    I3 = ChordEnum.C3,
    //ii3 = ChordEnum.Dm3,
    //iii3 = ChordEnum.Em3,
    //IV3 = ChordEnum.F3,
    //V3 = ChordEnum.G3,
    //vi3 = ChordEnum.Am4,
    //viiidim3 = ChordEnum.Bdim4,

    ////C4
    //I4 = ChordEnum.C4,
    //ii4 = ChordEnum.Dm4,
    //iii4 = ChordEnum.Em4,
    //IV4 = ChordEnum.F4,
    //V4 = ChordEnum.G4,

}

public enum ChordEnum
{
    none = 0,

    // C1 Major key
    C1,
    Dm1,
    Em1,
    F1,
    G1,

    // C2 Major key
    Am2,
    Bdim2,
    C2,
    Dm2,
    Em2,
    F2,
    G2,

    // C3 Major key
    Am3,
    Bdim3,
    C3,
    //Dm3,
    //Em3,
    //F3,
    //G3,

    //// C4 Major key
    //Am4,
    //Bdim4,
    //C4,
    //Dm4,
    //Em4,
    //F4,
    //G4,

}



public enum Note
{
    

    c1 = 48,
    cs1 = 49,
    db1 = 49,
    d1 = 50,
    ds1 = 51,
    eb1 = 51,
    e1 = 52,
    f1 = 53,
    fs1 = 54,
    gb1 = 54,
    g1 = 55,
    gs1 = 56,
    ab2 = 56,
    a2 = 57,
    as2 = 58,
    bb2 = 58,
    b2 = 59,

    c2 = 48 + 12, // 60
    cs2 = 49 + 12,
    db2 = 49 + 12,
    d2 = 50 + 12,
    ds2 = 51 + 12,
    eb2 = 51 + 12,
    e2 = 52 + 12,
    f2 = 53 + 12,
    fs2 = 54 + 12,
    gb2 = 54 + 12,
    g2 = 55 + 12,
    gs2 = 56 + 12,
    ab3 = 56 + 12,
    a3 = 57 + 12,
    as3 = 58 + 12,
    bb3 = 58 + 12,
    b3 = 59 + 12,

    c3 = 48 + 24, // 72
    cs3 = 49 + 24,
    db3 = 49 + 24,
    d3 = 50 + 24,
    ds3 = 51 + 24,
    eb3 = 51 + 24,
    e3 = 52 + 24,
    f3 = 53 + 24,
    fs3 = 54 + 24,
    gb3 = 54 + 24,
    g3 = 55 + 24,

    //gs3 = 56 + 24,
    //ab4 = 56 + 24,
    //a4 = 57 + 24,
    //as4 = 58 + 24,
    //bb4 = 58 + 24,
    //b4 = 59 + 24,

    //c4 = 48 + 36, // 84
    //cs4 = 49 + 36,
    //db4 = 49 + 36,
    //d4 = 50 + 36,
    //ds4 = 51 + 36,
    //eb4 = 51 + 36,
    //e4 = 52 + 36,
    //f4 = 53 + 36,
    //fs4 = 54 + 36,
    //gb4 = 54 + 36,
    //g4 = 55 + 36,
}


