using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    public GameObject TextList;
    GameObject[] Child = new GameObject[100];
    public string EnterTextHere;
    int SizeOfString;

    public GameObject TextAppearingPosRot1;
    public float GapBetweenCharacters;
    public float RotationInX;
    public float RotationInY;
    public float RotationInZ;

    float Half = 0.8f;
    float More = 1.35f;

    GameObject[] gameobjectInstantiated;
    GameObject TextAppearingPosRot;
    // Start is called before the first frame update
    void Start()
    {
        TextAppearingPosRot = new GameObject();
        RotationInX = - 73;
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///if You want to show any integer as 3D text then just use below line 
        //int speed=459;
        //EnterTextHere = speed.ToString();
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        TextAppearingPosRot.transform.position = new Vector3(TextAppearingPosRot1.transform.position.x - 3f, TextAppearingPosRot1.transform.position.y * 2 + 1f, TextAppearingPosRot1.transform.position.z);
        for (int i = 0; i <= 93; i++)
        {
            Child[i] = TextList.transform.GetChild(i).gameObject;
        }

        SizeOfString = EnterTextHere.Length;

        gameobjectInstantiated = new GameObject[SizeOfString];

        for (int j = 0; j < SizeOfString; j++)
        {
            char Character = EnterTextHere[j];

            switch (Character)
            {
                case '0':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[0], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '1':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[1], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case '2':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[2], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '3':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[3], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '4':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[4], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;

                case '5':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[5], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '6':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[6], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '7':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[7], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '8':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[8], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '9':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[9], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'a':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[10], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'b':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[11], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'c':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[12], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'd':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[13], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'e':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[14], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'f':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[15], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case 'g':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[16], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'h':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[17], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;

                case 'i':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[18], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half * .8f;
                    }
                    break;
                case 'j':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[19], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case 'k':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[20], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'l':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[21], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case 'm':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[22], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'n':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[23], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'o':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[24], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'p':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[25], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'q':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[26], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'r':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[27], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case 's':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[28], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 't':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[29], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case 'u':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[30], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'v':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[31], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'w':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[32], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'x':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[33], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'y':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[34], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'z':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[35], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '}':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[36], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'A':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[37], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'B':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[38], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'C':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[39], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'D':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[40], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'E':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[41], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'F':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[42], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'G':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[43], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'H':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[44], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'I':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[45], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'J':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[46], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case 'K':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[47], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'L':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[48], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'M':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[49], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'N':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[50], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'O':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[51], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'P':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[52], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'Q':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[53], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'R':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[54], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'S':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[55], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'T':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[56], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'U':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[57], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'V':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[58], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'W':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[59], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More * 1.5f;
                    }
                    break;
                case 'X':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[60], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'Y':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[61], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'Z':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[62], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case '!':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[63], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case '"':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[64], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '#':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[65], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '$':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[66], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '%':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[67], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case '&':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[68], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '\'':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[69], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '(':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[70], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case ')':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[71], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '*':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[72], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case '+':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[73], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case ',':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[74], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case '-':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[75], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case ':':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[76], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case ';':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[77], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case '<':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[78], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '=':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[79], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '>':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[80], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '?':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[81], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '@':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[82], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More * 1.5f;
                    }
                    break;
                case '[':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[83], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '\\':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[84], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case ']':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[85], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '^':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[86], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case '_':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[87], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '`':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[88], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case '.':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[89], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case '/':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[90], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '{':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[91], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '|':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[92], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case '~':
                    {
                        gameobjectInstantiated[j] = Instantiate(Child[93], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameobjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);gameobjectInstantiated[j].transform.parent = this.transform;
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case ' ':
                    {

                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;



            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        //TextAppearingPosRot.transform.position = new Vector3(TextAppearingPosRot1.transform.position.x - 3f, TextAppearingPosRot1.transform.position.y * 2 + 1f, TextAppearingPosRot1.transform.position.z);

    }
}
