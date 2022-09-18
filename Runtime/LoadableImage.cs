using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Kogane
{
    /// <summary>
    /// スプライトの非同期読み込みに対応した Image を管理するコンポーネントの抽象クラス
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent( typeof( Image ) )]
    public abstract class LoadableImageBase : MonoBehaviour
    {
        //================================================================================
        // 変数(readonly)
        //================================================================================
        private ILoadableImageLoader m_loader;

        //==============================================================================
        // 変数
        //==============================================================================
        private Image  m_imageCache;
        private string m_currentPath;

        //================================================================================
        // プロパティ
        //================================================================================
        private Image Image
        {
            get
            {
                if ( m_imageCache == null )
                {
                    m_imageCache = GetComponent<Image>();
                }

                return m_imageCache;
            }
        }

        //================================================================================
        // 関数
        //================================================================================
        /// <summary>
        /// 破棄される時に呼び出されます
        /// </summary>
        protected void OnDestroy()
        {
            m_loader?.Dispose();
            m_loader = null;

            DoOnDestroy();
        }

        /// <summary>
        /// 破棄される時に呼び出されます
        /// </summary>
        protected virtual void DoOnDestroy()
        {
        }

        /// <summary>
        /// 表示を設定します
        /// </summary>
        public async UniTask SetupAsync( string path )
        {
            if ( m_currentPath == path ) return;

            m_loader ??= CreateAssetLoader();

            Image.sprite  = await m_loader.LoadAsync( gameObject, path );
            m_currentPath = path;
        }

        /// <summary>
        /// 表示を設定します
        /// </summary>
        public void Setup( string path )
        {
            SetupAsync( path ).Forget();
        }

        /// <summary>
        /// アセットを読み込むインスタンスを作成して返します
        /// </summary>
        protected abstract ILoadableImageLoader CreateAssetLoader();
    }
}