using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutsceneAnimator : MonoBehaviour
{
    public CanvasGroup[] fadeImages; // P1-3
    public RectTransform[] slideImages; // P4-6
    public RectTransform explosionImage; // P7
    public RectTransform dropImage; // P8
    public CanvasGroup[] screamImages; // P9–11
    public CanvasGroup[] screamFrames9To11;
    public CanvasGroup staticImage12;            // P12
    public CanvasGroup[] runImages13And14;//1314

    public float fadeTime = 1f;
    public float displayTime = 1f;

    void Start()
    {
        StartCoroutine(PlayCutscene());
    }

    IEnumerator PlayCutscene()
    {
        for (int i = 3; i <= 5; i++)
        {
            if (fadeImages[i] != null)
            {
                fadeImages[i].gameObject.SetActive(false);
            }
        }
        CanvasGroup[] introImages = new CanvasGroup[] { fadeImages[0], fadeImages[1], fadeImages[2] };
        for (int i = 0; i < introImages.Length; i++)
        {
            yield return StartCoroutine(FadeIn(introImages[i]));
            yield return new WaitForSeconds(displayTime);
        }


        yield return StartCoroutine(FadeOutAll(fadeImages));


        CanvasGroup[] runFrames = new CanvasGroup[] { fadeImages[3], fadeImages[4], fadeImages[5] };

        foreach (CanvasGroup cg in runFrames)
        {
            if (cg != null)
            {
                cg.gameObject.SetActive(true);
                cg.alpha = 0;
            }
        }
        yield return StartCoroutine(PlayRunLoop(runFrames, 3, 0.15f));


        foreach (RectTransform rt in slideImages)
        {
            yield return StartCoroutine(SlideInFromRight_RunEffect(rt));
            yield return new WaitForSeconds(displayTime);
        }

        yield return StartCoroutine(FadeOutRectTransformList(slideImages));


        yield return StartCoroutine(ExplodeIn(explosionImage));
        yield return new WaitForSeconds(displayTime);

        CanvasGroup explosionCG = explosionImage.GetComponent<CanvasGroup>();
        if (explosionCG != null)
            explosionCG.alpha = 1f;


        yield return StartCoroutine(SlideInFromTop(dropImage));

        yield return new WaitForSeconds(displayTime);


        CanvasGroup dropCG = dropImage.GetComponent<CanvasGroup>();


        if (explosionCG != null && dropCG != null)
        {
            yield return StartCoroutine(FadeOutCanvasGroupList(new CanvasGroup[] { explosionCG, dropCG }));
        }

        // Step 1: scream
        foreach (CanvasGroup cg in screamFrames9To11)
        {
            if (cg != null)
            {
                cg.alpha = 0;
                cg.gameObject.SetActive(true);
            }
        }

        // Step 2: 
        yield return StartCoroutine(PlayRunLoop(screamFrames9To11, 4, 0.1f));

        // Step 3: 
        yield return StartCoroutine(FadeOutCanvasGroupList(screamFrames9To11));

        if (staticImage12 != null)
        {
            staticImage12.alpha = 0;
            staticImage12.gameObject.SetActive(true);
            yield return StartCoroutine(FadeIn(staticImage12));
            yield return new WaitForSeconds(displayTime);
        }



        // --- 13、14RUN
        foreach (CanvasGroup cg in runImages13And14)
        {
            if (cg != null)
            {
                cg.alpha = 0;
                cg.gameObject.SetActive(true);
            }
        }


        yield return StartCoroutine(PlayRunLoop(runImages13And14, 9, 0.15f, hideAfter: false));
        
		yield return new WaitForSeconds(2f);
		SceneManager.LoadScene("LoadingScene");

    }
    IEnumerator FadeOut(CanvasGroup cg)
    {
        float t = 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            cg.alpha = 1 - (t / fadeTime);
            yield return null;
        }
        cg.alpha = 0;
    }
    IEnumerator FadeIn(CanvasGroup cg)
    {
        cg.alpha = 0; // 
        float t = 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Lerp(0f, 1f, t / fadeTime);
            yield return null;
        }
        cg.alpha = 1f;
    }
    IEnumerator FadeOutAll(CanvasGroup[] canvasGroups)
    {
        float t = 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            float alpha = 1 - (t / fadeTime);
            foreach (CanvasGroup cg in canvasGroups)
            {
                cg.alpha = alpha;
            }
            yield return null;
        }

        foreach (CanvasGroup cg in canvasGroups)
        {
            cg.alpha = 0;
        }
    }
    IEnumerator FadeOutCanvasGroupList(CanvasGroup[] canvasGroups)
    {
        float t = 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            float alpha = 1 - (t / fadeTime);
            foreach (CanvasGroup cg in canvasGroups)
            {
                if (cg != null)
                    cg.alpha = alpha;
            }
            yield return null;
        }

        foreach (CanvasGroup cg in canvasGroups)
        {
            if (cg != null)
                cg.alpha = 0;
        }
    }


    IEnumerator FadeOutRectTransformList(RectTransform[] rects)
    {
        float t = 0f;
        CanvasGroup[] canvasGroups = new CanvasGroup[rects.Length];

        for (int i = 0; i < rects.Length; i++)
            canvasGroups[i] = rects[i].GetComponent<CanvasGroup>();

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            float alpha = 1 - (t / fadeTime);
            foreach (CanvasGroup cg in canvasGroups)
            {
                if (cg != null)
                    cg.alpha = alpha;
            }
            yield return null;
        }

        foreach (CanvasGroup cg in canvasGroups)
        {
            if (cg != null)
                cg.alpha = 0;
        }
    }
    IEnumerator FadeOut(RectTransform[] rects)
    {
        float t = 0f;
        CanvasGroup[] canvasGroups = new CanvasGroup[rects.Length];
        for (int i = 0; i < rects.Length; i++)
        {
            canvasGroups[i] = rects[i].GetComponent<CanvasGroup>();
        }

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            float alpha = 1 - (t / fadeTime);
            foreach (CanvasGroup cg in canvasGroups)
            {
                if (cg != null)
                    cg.alpha = alpha;
            }
            yield return null;
        }

        foreach (CanvasGroup cg in canvasGroups)
        {
            if (cg != null)
                cg.alpha = 0;
        }
    }

    IEnumerator SlideInFromRight_RunEffect(RectTransform rt, float overshoot = 30f)
    {
        CanvasGroup cg = rt.GetComponent<CanvasGroup>();
        if (cg != null) cg.alpha = 1f;

        Vector2 finalPos = rt.anchoredPosition;
        Vector2 startPos = finalPos + new Vector2(600f, 0);


        Vector2 overshootPos = finalPos - new Vector2(overshoot, 0);

        float t = 0;
        float duration = fadeTime * 0.6f;


        while (t < duration)
        {
            t += Time.deltaTime;
            rt.anchoredPosition = Vector2.Lerp(startPos, overshootPos, t / duration);
            yield return null;
        }


        t = 0;
        float settleDuration = fadeTime * 0.4f;
        while (t < settleDuration)
        {
            t += Time.deltaTime;
            rt.anchoredPosition = Vector2.Lerp(overshootPos, finalPos, t / settleDuration);
            yield return null;
        }

        rt.anchoredPosition = finalPos;
    }

    IEnumerator SlideInFromTop(RectTransform rt)
    {
        CanvasGroup cg = rt.GetComponent<CanvasGroup>();
        if (cg != null) cg.alpha = 1f;
        Vector3 startPos = new Vector3(rt.anchoredPosition.x, Screen.height + 300, 0);
        Vector3 endPos = rt.anchoredPosition;

        rt.anchoredPosition = startPos;

        float t = 0;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            rt.anchoredPosition = Vector3.Lerp(startPos, endPos, t / fadeTime);
            yield return null;
        }
        rt.anchoredPosition = endPos;
    }
    IEnumerator FadeOutCanvasGroup(CanvasGroup cg)
    {
        float t = 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            cg.alpha = 1 - (t / fadeTime);
            yield return null;
        }
        cg.alpha = 0;
    }
    IEnumerator ExplodeIn(RectTransform rt)
    {

        CanvasGroup cg = rt.GetComponent<CanvasGroup>();
        if (cg != null) cg.alpha = 1f;

        rt.localScale = Vector3.zero;
        Vector2 originalPos = rt.anchoredPosition;

        float t = 0;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            float progress = t / fadeTime;

            rt.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, progress);

            float shakeAmount = 10f * (1f - progress);
            float offsetX = Mathf.Sin(t * 30f) * shakeAmount;
            rt.anchoredPosition = originalPos + new Vector2(offsetX, 0f);

            yield return null;
        }

        rt.localScale = Vector3.one;
        rt.anchoredPosition = originalPos;
    }

    IEnumerator PlayRunLoop(CanvasGroup[] runFrames, int loopCount, float frameDuration, bool hideAfter = true)
    {
        int lastIndex = runFrames.Length - 1;

        for (int loop = 0; loop < loopCount; loop++)
        {
            for (int i = 0; i < runFrames.Length; i++)
            {
                for (int j = 0; j < runFrames.Length; j++)
                    runFrames[j].alpha = (j == i) ? 1f : 0f;

                yield return new WaitForSeconds(frameDuration);
            }
        }

        if (hideAfter)
        {
            foreach (var cg in runFrames)
                cg.alpha = 0f;
        }
        else
        {
            for (int i = 0; i < runFrames.Length; i++)
                runFrames[i].alpha = (i == lastIndex) ? 1f : 0f;
        }
    }
}
