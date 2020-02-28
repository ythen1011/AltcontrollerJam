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
            chordname = functionalChords[key][function][Random.Range((int)0, functionalChords[key][function].Count)]; // get a random chord of this function
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

            Debug.LogError("chord does not exist: " + chordname.ToString());
            return null;
        }
        
             
    }

    private void Start()
    {

        // C Major

        // C1 Major
        allChords[ChordEnum.C1] = new List<Note> { Note.c1, Note.e1, Note.g1  };
        allChords[ChordEnum.Dm1] = new List<Note> { Note.d1, Note.f1, Note.a2 };
        allChords[ChordEnum.Em1] = new List<Note> { Note.e1, Note.g1, Note.b2 };
        allChords[ChordEnum.F1] = new List<Note> { Note.f1, Note.a2, Note.c2  };
        allChords[ChordEnum.G1] = new List<Note> { Note.g1, Note.b2, Note.d2  };

        // C2 Major
        allChords[ChordEnum.Am2] = new List<Note> { Note.a2, Note.c2, Note.e2    };
        allChords[ChordEnum.Bdim2] = new List<Note> { Note.b2, Note.d2, Note.f2  };
        allChords[ChordEnum.C2] = new List<Note> { Note.c2, Note.e2, Note.g2     };
        allChords[ChordEnum.Dm2] = new List<Note> { Note.d2, Note.f2, Note.a3    };
        allChords[ChordEnum.Em2] = new List<Note> { Note.e2, Note.g2, Note.b3    };
        allChords[ChordEnum.F2] = new List<Note> { Note.f2, Note.a3, Note.c3     };
        allChords[ChordEnum.G2] = new List<Note> { Note.g2, Note.b3, Note.d3     };

        // C3 Major
        allChords[ChordEnum.Am3] = new List<Note> { Note.a3, Note.c3, Note.e3,  };
        allChords[ChordEnum.Bdim3] = new List<Note> { Note.b3, Note.d3, Note.f3 };  
        allChords[ChordEnum.C3] = new List<Note> { Note.c3, Note.e3, Note.g3 }; 
        

        // C Major 7ths

        // C1 Major 7ths
         allChords[ChordEnum.C71] = new List<Note> { Note.c1, Note.e1, Note.g1 , Note.b2 };
        allChords[ChordEnum.Dm71] = new List<Note> { Note.d1, Note.f1, Note.a2, Note.c2 };
        allChords[ChordEnum.Em71] = new List<Note> { Note.e1, Note.g1, Note.b2, Note.d2 };
         allChords[ChordEnum.F71] = new List<Note> { Note.f1, Note.a2, Note.c2 , Note.e2 };
         allChords[ChordEnum.G71] = new List<Note> { Note.g1, Note.b2, Note.d2 , Note.f2 };

        // C2 Major 7ths
          allChords[ChordEnum.Am72] = new List<Note> { Note.a2, Note.c2, Note.e2   , Note.g2 };
        allChords[ChordEnum.Bhalfdim72] = new List<Note> { Note.b2, Note.d2, Note.f2 , Note.a3 };
           allChords[ChordEnum.C72] = new List<Note> { Note.c2, Note.e2, Note.g2    , Note.b3 };
          allChords[ChordEnum.Dm72] = new List<Note> { Note.d2, Note.f2, Note.a3   , Note.c3 };
          allChords[ChordEnum.Em72] = new List<Note> { Note.e2, Note.g2, Note.b3   , Note.d3 };
           allChords[ChordEnum.F72] = new List<Note> { Note.f2, Note.a3, Note.c3    , Note.e3 };
           allChords[ChordEnum.G72] = new List<Note> { Note.g2, Note.b3, Note.d3    , Note.f3 };

        // C3 Major 7ths
        allChords[ChordEnum.Am73] = new List<Note> { Note.a3, Note.c3, Note.e3, Note.g3 };
      


        // C Minor

        // C1 Minor
          allChords[ChordEnum.Cm1] = new List<Note> {  Note.c1, Note.eb1,  Note.g1 };
        allChords[ChordEnum.Ddim1] = new List<Note> {  Note.d1,  Note.f1, Note.ab2 };
          allChords[ChordEnum.Eb1] = new List<Note> { Note.eb1,  Note.g1, Note.bb2 };
          allChords[ChordEnum.Fm1] = new List<Note> {  Note.f1, Note.ab2 , Note.c2 };
          allChords[ChordEnum.Gm1] = new List<Note> {  Note.g1, Note.bb2,  Note.d2 };

        // C2 Minor
          allChords[ChordEnum.Ab2] = new List<Note> { Note.ab2, Note.c2, Note.eb2 };
          allChords[ChordEnum.Bb2] = new List<Note> { Note.bb2, Note.d2, Note.f2 };
          allChords[ChordEnum.Cm2] = new List<Note> {  Note.c2, Note.eb2,  Note.g2 };
        allChords[ChordEnum.Ddim2] = new List<Note> {  Note.d2,  Note.f2, Note.ab3 };
          allChords[ChordEnum.Eb2] = new List<Note> { Note.eb2,  Note.g2, Note.bb3 };
          allChords[ChordEnum.Fm2] = new List<Note> {  Note.f2, Note.ab3 , Note.c3 };
          allChords[ChordEnum.Gm2] = new List<Note> {  Note.g2, Note.bb3,  Note.d3 };

        // C3 Minor
        allChords[ChordEnum.Ab3] = new List<Note> { Note.ab3, Note.c3, Note.eb3};
        allChords[ChordEnum.Bb3] = new List<Note> { Note.bb3, Note.d3, Note.f3 };
        allChords[ChordEnum.Cm3] = new List<Note> { Note.c3, Note.eb3, Note.g3 };




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



        // C Minor functional chords

        functionalChords[MusicalKey.CMinor] = new Dictionary<ChordFunction, List<ChordEnum>>();
        functionalChords[MusicalKey.CMinor][ChordFunction.root] = new List<ChordEnum> { ChordEnum.Cm1, ChordEnum.Cm2, ChordEnum.Cm3, };

        functionalChords[MusicalKey.CMinor][ChordFunction.tonic] = new List<ChordEnum> {
            (ChordEnum)FunctionalCMinor.i1,       (ChordEnum)FunctionalCMinor.i2,     (ChordEnum)FunctionalCMinor.i3,
            (ChordEnum)FunctionalCMinor.III1,     (ChordEnum)FunctionalCMinor.III2,
        }; 
        
        functionalChords[MusicalKey.CMinor][ChordFunction.subdominant] = new List<ChordEnum> {
            (ChordEnum)FunctionalCMinor.iv1,       (ChordEnum)FunctionalCMinor.iv2,
            (ChordEnum)FunctionalCMinor.VI1,     (ChordEnum)FunctionalCMinor.VI2,
            (ChordEnum)FunctionalCMinor.iidim1,     (ChordEnum)FunctionalCMinor.iidim2,
        }; 
        
        functionalChords[MusicalKey.CMinor][ChordFunction.dominant] = new List<ChordEnum> {
            (ChordEnum)FunctionalCMinor.v1,       (ChordEnum)FunctionalCMinor.v2,
            (ChordEnum)FunctionalCMinor.VII1,     (ChordEnum)FunctionalCMinor.VII2,
            
        };

    }



}


public enum MusicalKey
{
    none = 0,

    CMajor,
    CMinor,
    count
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

    //C2
    I2 = ChordEnum.C2,
    ii2 = ChordEnum.Dm2,
    iii2 = ChordEnum.Em2,
    IV2 = ChordEnum.F2,
    V2 = ChordEnum.G2,
    vi2 = ChordEnum.Am3,
    viiidim2 = ChordEnum.Bdim3,

    //C3
    I3 = ChordEnum.C3,
    
    //C1 7ths
          I71 =    ChordEnum.C1,
         ii71 =   ChordEnum.Dm1,
        iii71 =   ChordEnum.Em1,
         IV71 =    ChordEnum.F1,
          V71 =    ChordEnum.G1,
         vi71 =   ChordEnum.Am2,
    viiidim71 = ChordEnum.Bdim2,
    
    //C2 7ths
          I72 =    ChordEnum.C2,
         ii72 =   ChordEnum.Dm2,
        iii72 =   ChordEnum.Em2,
         IV72 =    ChordEnum.F2,
          V72 =    ChordEnum.G2,
         vi72 =   ChordEnum.Am3,
    viiidim72 = ChordEnum.Bdim3,

    //C2 7ths
    I73 = ChordEnum.C3,



}

public enum FunctionalCMinor
{
    none = 0,

        //Cm1
        i1 =   ChordEnum.Cm1,
    iidim1 = ChordEnum.Ddim1,
      III1 =   ChordEnum.Eb1,
       iv1 =   ChordEnum.Fm1,
        v1 =   ChordEnum.Gm1,
       VI1 =   ChordEnum.Ab2,
      VII1 =   ChordEnum.Bb2,
      
      //Cm2
        i2 =   ChordEnum.Cm2,
    iidim2 = ChordEnum.Ddim2,
      III2 =   ChordEnum.Eb2,
       iv2 =   ChordEnum.Fm2,
        v2 =   ChordEnum.Gm2,
       VI2 =   ChordEnum.Ab3,
      VII2 =   ChordEnum.Bb3, 
      
      //Cm3
        i3 =   ChordEnum.Cm3,
       

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


    // C1 7ths Major key
       C71,
      Dm71,
      Em71,
       F71,
       G71,

    // C2 7ths Major key
      Am72,
    Bhalfdim72,
       C72,
      Dm72,
      Em72,
       F72,
       G72,
       
       // C3 7ths Major key
      Am73,
  

    // C1 Minor key
      Cm1,
    Ddim1,
      Eb1,
      Fm1,
      Gm1,


     // C2 Minor key
      Ab2,
     Bb2,
      Cm2,
    Ddim2,
      Eb2,
      Fm2,
      Gm2,
      
      // C2 Minor key
      Ab3,
     Bb3,
      Cm3,
  

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

}
