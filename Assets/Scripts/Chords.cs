using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chords : MonoBehaviour
{
    private static Chords instance = null;

    public static Chords Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Chords();
            }
            return instance;
        }
    }



    Dictionary<chord, List<Note>> allChords = new Dictionary<chord, List<Note>>();
    Dictionary<functionalCMajor, Dictionary<chord, List<Note>>> cMajorChords = new Dictionary<functionalCMajor, Dictionary<chord, List<Note>>>();


    private void Start()
    {

        // C1 Major

        allChords[chord.C1] = new List<Note> { Note.c1, Note.e2, Note.g1 };
        allChords[chord.Dm1] = new List<Note> { Note.d1, Note.f2, Note.a2 };
        allChords[chord.Em1] = new List<Note> { Note.e1, Note.g2, Note.b2 };
        allChords[chord.F1] = new List<Note> { Note.f1, Note.a2, Note.c2 };
        allChords[chord.G1] = new List<Note> { Note.g1, Note.b2, Note.d2 };

        // C2 Major
        allChords[chord.Am2] = new List<Note> { Note.a2, Note.c2, Note.e2 };
        allChords[chord.Bdim2] = new List<Note> { Note.b2, Note.d2, Note.f2 };
        allChords[chord.C2] = new List<Note> { Note.c2, Note.e2, Note.g2 };
        allChords[chord.Dm2] = new List<Note> { Note.d2, Note.f2, Note.a3 };
        allChords[chord.Em2] = new List<Note> { Note.e2, Note.g2, Note.b3 };
        allChords[chord.F2] = new List<Note> { Note.f2, Note.a3, Note.c3 };
        allChords[chord.G2] = new List<Note> { Note.g2, Note.b3, Note.d3 };

        // C3 Major
        allChords[chord.Am3] = new List<Note> { Note.a3, Note.c3, Note.e3 };
        allChords[chord.Bdim3] = new List<Note> { Note.b3, Note.d3, Note.f3 };
        allChords[chord.C3] = new List<Note> { Note.c3, Note.e3, Note.g3 };
        allChords[chord.Dm3] = new List<Note> { Note.d3, Note.f3, Note.a4 };
        allChords[chord.Em3] = new List<Note> { Note.e3, Note.g3, Note.b4 };
        allChords[chord.F3] = new List<Note> { Note.f3, Note.a4, Note.c4 };
        allChords[chord.G3] = new List<Note> { Note.g3, Note.b4, Note.d4 };

        // C4 Major
        allChords[chord.Am4] = new List<Note> { Note.a4, Note.c4, Note.e4 };
        allChords[chord.Bdim4] = new List<Note> { Note.b4, Note.d4, Note.f4 };
        allChords[chord.C4] = new List<Note> { Note.c4, Note.e4, Note.g4 };




    }



}


enum functionalCMajor
{
    //C1
    I1 = chord.C1,
    ii1 = chord.Dm1,
    iii1 = chord.Em1,
    IV1 = chord.F1,
    V1 = chord.G1,
    vi1 = chord.Am2,
    viiidim1 = chord.Bdim2,

    //C1
    I2 = chord.C2,
    ii2 = chord.Dm2,
    iii2 = chord.Em2,
    IV2 = chord.F2,
    V2 = chord.G2,
    vi2 = chord.Am3,
    viiidim2 = chord.Bdim3,

    //C3
    I3 = chord.C3,
    ii3 = chord.Dm3,
    iii3 = chord.Em3,
    IV3 = chord.F3,
    V3 = chord.G3,
    vi3 = chord.Am4,
    viiidim3 = chord.Bdim4,

    //C4
    I4 = chord.C4,
    ii4 = chord.Dm4,
    iii4 = chord.Em4,
    IV4 = chord.F4,
    V4 = chord.G4,

}

enum chord
{
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

    // C2 Major key
    Am3,
    Bdim3,
    C3,
    Dm3,
    Em3,
    F3,
    G3,

    // C2 Major key
    Am4,
    Bdim4,
    C4,
    Dm4,
    Em4,
    F4,
    G4,

}



enum Note
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
    gs3 = 56 + 24,
    ab4 = 56 + 24,
    a4 = 57 + 24,
    as4 = 58 + 24,
    bb4 = 58 + 24,
    b4 = 59 + 24,

    c4 = 48 + 36, // 84
    cs4 = 49 + 36,
    db4 = 49 + 36,
    d4 = 50 + 36,
    ds4 = 51 + 36,
    eb4 = 51 + 36,
    e4 = 52 + 36,
    f4 = 53 + 36,
    fs4 = 54 + 36,
    gb4 = 54 + 36,
    g4 = 55 + 36,
}


