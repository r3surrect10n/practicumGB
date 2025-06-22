using UnityEngine;
using UnityEngine.UI;

public class SelectNewsControl : MonoBehaviour
{
    [SerializeField] private Sprite[] arrPage;
    [SerializeField] private Image imgPage;
    [SerializeField] private Image logo;
    [SerializeField] private Material combineMaterial; // �������� ��� ��������� ��������


    private int currentPageIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (arrPage.Length > 0) imgPage.sprite = arrPage[0];
        /*if (arrPage.Length > 1)
        {
            arrPage[1] = CombineSprites(arrPage[1], logo.sprite);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickNext()
    {
        currentPageIndex++;
        currentPageIndex %= arrPage.Length;
        imgPage.sprite = arrPage[currentPageIndex];
        //logo.gameObject.SetActive(currentPageIndex == 1);
    }

    public void OnClickPrev()
    {
        currentPageIndex += arrPage.Length - 1;
        currentPageIndex %= arrPage.Length;
        imgPage.sprite = arrPage[currentPageIndex];
        //logo.gameObject.SetActive(currentPageIndex == 1);
    }

    public void OnClickSelect()
    {
        QuestObject qo = gameObject.GetComponent<QuestObject>();
        if (qo != null)
        {
            if (currentPageIndex == 1) qo.ChangeStatus(QuestStatus.isSuccess);
            else qo.ChangeStatus(QuestStatus.isFailed);

        }
    }

    public Sprite CombineSprites(Sprite first, Sprite second)
    {
        // ������� �������� ������-�������� �� ������� ������� �������
        RenderTexture renderTexture = RenderTexture.GetTemporary(
            first.texture.width,
            first.texture.height,
            0,
            RenderTextureFormat.ARGB32
        );

        // ������������� ������ ������ �� ��� ����������� ������-��������
        Graphics.Blit(first.texture, renderTexture, combineMaterial);

        // ���������� ������ �������� (���������) ��� ������������ ���������
        Texture2D smallTexture = second.texture as Texture2D;

        // ������� ������� �������������� ��� ���������� �������
        Matrix4x4 matrix = Matrix4x4.TRS(
            new Vector3(second.rect.xMin, second.rect.yMin),
            Quaternion.identity,
            new Vector3(smallTexture.width / (float)first.texture.width, smallTexture.height / (float)first.texture.height, 1)
        );

        // ����������� ��������� �������� ������ ������, �������� �� ������������ ����������
        //Graphics.DrawTexture(new Rect(second.rect.xMin, second.rect.yMin, smallTexture.width, smallTexture.height), smallTexture, matrix);
        Graphics.DrawTexture(new Rect(second.rect.xMin, second.rect.yMin, smallTexture.width, smallTexture.height), smallTexture, combineMaterial, 1);

        // ������ ������� �� ������-�������� � ��������� �� � ����� ��������
        Texture2D textureResult = new Texture2D(first.texture.width, first.texture.height, TextureFormat.RGBA32, false);
        RenderTexture.active = renderTexture;
        textureResult.ReadPixels(new Rect(0, 0, first.texture.width, first.texture.height), 0, 0);
        textureResult.Apply();

        // ������� �������� ������
        Sprite combinedSprite = Sprite.Create(textureResult, new Rect(0, 0, textureResult.width, textureResult.height), new Vector2(0.5f, 0.5f));

        // ����������� ��������� ������-��������
        RenderTexture.ReleaseTemporary(renderTexture);

        return combinedSprite;
    }

}
