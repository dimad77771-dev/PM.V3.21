using System.Globalization;
using System.IO;
using DevExpress.Internal;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.RichEdit;
using DevExpress.Xpf.SpellChecker;
using DevExpress.XtraRichEdit.SpellChecker;
using DevExpress.XtraSpellChecker;
using DevExpress.XtraSpellChecker.Native;

namespace DevExpress.DevAV {
    public class SpellCheckerRichEditBehavior : Behavior<RichEditControl>{
        SpellChecker spellChecker;
        protected override void OnAttached() {
            base.OnAttached();
            AssociatedObject.SpellChecker = spellChecker;
        }
        public SpellCheckerRichEditBehavior() {
            spellChecker = CreateSpellChecker();
        }
        SpellChecker CreateSpellChecker() {
            SpellChecker spellChecker = new SpellChecker();
            spellChecker.Culture = new CultureInfo("en-US");
            spellChecker.SpellCheckMode = SpellCheckMode.AsYouType;
            RegisterDictionary(spellChecker, GetDefaultDictionary());
            SpellCheckTextControllersManager.Default.RegisterClass(typeof(RichEditControl), typeof(RichEditSpellCheckController));
            return spellChecker;
        }
        SpellCheckerDictionaryBase GetDefaultDictionary() {
            SpellCheckerISpellDictionary dic = new SpellCheckerISpellDictionary();
            dic.LoadFromStream(GetFileStream("american.xlg"),GetFileStream("english.aff"),GetFileStream("EnglishAlphabet.txt"));
            return dic;
        }
        static Stream GetFileStream(string path) {
            return File.OpenRead(DataDirectoryHelper.GetFile(path, DataDirectoryHelper.DataFolderName));
        }
        void RegisterDictionary(SpellChecker spellChecker, SpellCheckerDictionaryBase dic) {
            dic.Culture = spellChecker.Culture;
            spellChecker.Dictionaries.Add(dic);
        }
    }
}
