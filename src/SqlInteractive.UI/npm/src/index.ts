import {
	EditorView,
	keymap, highlightSpecialChars, drawSelection, highlightActiveLine, dropCursor,
	rectangularSelection, crosshairCursor,
	lineNumbers, highlightActiveLineGutter
} from "@codemirror/view"
import { Extension, EditorState, Compartment } from "@codemirror/state"
import {
	defaultHighlightStyle, syntaxHighlighting, indentOnInput, bracketMatching,
	foldGutter, foldKeymap, HighlightStyle
} from "@codemirror/language"
import { defaultKeymap, history, historyKeymap } from "@codemirror/commands"
import { searchKeymap, highlightSelectionMatches } from "@codemirror/search"
import { autocompletion, completionKeymap, closeBrackets, closeBracketsKeymap } from "@codemirror/autocomplete"
import { lintKeymap } from "@codemirror/lint"
import { sql } from "@codemirror/lang-sql"
import { tags as t } from "@lezer/highlight"

/// This is an extension value that just pulls together a number of
/// extensions that you might want in a basic editor. It is meant as a
/// convenient helper to quickly set up CodeMirror without installing
/// and importing a lot of separate packages.
///
/// Specifically, it includes...
///
///  - [the default command bindings](#commands.defaultKeymap)
///  - [line numbers](#view.lineNumbers)
///  - [special character highlighting](#view.highlightSpecialChars)
///  - [the undo history](#commands.history)
///  - [a fold gutter](#language.foldGutter)
///  - [custom selection drawing](#view.drawSelection)
///  - [drop cursor](#view.dropCursor)
///  - [multiple selections](#state.EditorState^allowMultipleSelections)
///  - [reindentation on input](#language.indentOnInput)
///  - [the default highlight style](#language.defaultHighlightStyle) (as fallback)
///  - [bracket matching](#language.bracketMatching)
///  - [bracket closing](#autocomplete.closeBrackets)
///  - [autocompletion](#autocomplete.autocompletion)
///  - [rectangular selection](#view.rectangularSelection) and [crosshair cursor](#view.crosshairCursor)
///  - [active line highlighting](#view.highlightActiveLine)
///  - [active line gutter highlighting](#view.highlightActiveLineGutter)
///  - [selection match highlighting](#search.highlightSelectionMatches)
///  - [search](#search.searchKeymap)
///  - [linting](#lint.lintKeymap)
///
/// (You'll probably want to add some language package to your setup
/// too.)
///
/// This package does not allow customization. The idea is that, once
/// you decide you want to configure your editor more precisely, you
/// take this package's source (which is just a bunch of imports and
/// an array literal), copy it into your own code, and adjust it as
/// desired.

let language = new Compartment

var theme = EditorView.theme({
	".cm-editor .cm-content": {
			fontFamily: "SFMono-Regular",
	},
	".cm-content": {
		background: "none",
		border: "none",
		caretColor: "#111"
	},
	".cm-line": {
		height: "20px",
		fontSize: "14px",
		left: 0,
		top: 0,
		fontFamily: "'Cousine', monospace"
	},
	".cm-gutters": {
		fontSize: "12px",
		background: "#FFFFFF"
	},
	".cm-selectionLayer": {
		border: "none"
	},
	".cm-tooltip": {
		border: "1px solid #d4d4fb",
		borderRadius: "3px",
	},
	".cm-tooltip-autocomplete ul": {
		scrollbarColor: "#6969dd #e0e0e0",
		scrollbarWidth: "sthin",
		borderRadius: "3px",
	},
	".cm-tooltip-autocomplete ul::-webkit-scrollbar": {
		width: "7px",
		backgroundColor: "#ffffff00"
	},
	".cm-tooltip-autocomplete ul::-webkit-scrollbar-track": {
		backgroundColor: "#f0f0f000"
	},
	".cm-tooltip-autocomplete ul::-webkit-scrollbar-thumb": {
		boxShadow: "inset 0 0 6px rgba(0, 0, 0, 0.3)",
		borderRadius: "20px"
	},
	".cm-scroller, .cm-content, .cm-gutter": {
		overflow: "auto",
		minHeight: "200px",
	},
	".cm-activeLine": {
		background: "rgb(255 255 255)"
	}
}, { dark: false })

const highlight = HighlightStyle.define(
	[
		{
			tag: t.keyword,
			color: "#8500ff"
		},
		{
			tag: [t.name, t.deleted, t.character, t.macroName],
			color: "#4f559c"
		},
		{
			tag: t.propertyName,
			color: "#4f559c"
		},
		{
			tag: [t.definition(t.name), t.separator],
			color: "#4a4a4a"
		},
		{
			tag: [t.typeName, t.className, t.number, t.changed, t.annotation, t.modifier, t.self, t.namespace],
			color: "#8500ff"
		},
		{
			tag: [t.operator, t.operatorKeyword, t.url, t.escape, t.regexp, t.link, t.special(t.string)],
			color: "#41ad8f"
		},
		{
			tag: [t.meta, t.comment],
			color: "#9badb7"
		},
		{
			tag: t.strong,
			fontWeight: "bold"
		},
		{
			tag: t.emphasis,
			fontStyle: "italic"
		},
		{
			tag: t.strikethrough,
			textDecoration: "line-through"
		},
		{
			tag: [t.atom, t.bool, t.special(t.variableName)],
			color: "#8500ff"
		},
		{
			tag: [t.processingInstruction, t.string, t.inserted],
			color: "#41ad8f"
		}
	]
)
const basicSetup: Extension = [
	lineNumbers(),
	highlightActiveLineGutter(),
	highlightSpecialChars(),
	history(),
	foldGutter(),
	drawSelection(),
	dropCursor(),
	EditorState.allowMultipleSelections.of(true),
	indentOnInput(),
	syntaxHighlighting(defaultHighlightStyle, { fallback: true }),
	syntaxHighlighting(highlight),
	bracketMatching(),
	closeBrackets(),
	autocompletion(),
	rectangularSelection(),
	crosshairCursor(),
	highlightActiveLine(),
	highlightSelectionMatches(),
	keymap.of([
		...closeBracketsKeymap,
		...defaultKeymap,
		...searchKeymap,
		...historyKeymap,
		...foldKeymap,
		...completionKeymap,
		...lintKeymap
	]),
	theme,
	language.of(sql({ upperCaseKeywords: true }))
]

let startState = EditorState.create({
	doc: "SELECT 'Hello World'",
	extensions: [basicSetup]
})

let view = new EditorView({ state: startState });

let schemas: { [table: string]: readonly string[] };

let appendSqlEditor = function (wrapperId: string) {
	var wrapper = document.getElementById(wrapperId);
	wrapper.appendChild(view.dom);
}
let getEditorContent = function()
{
	var doc = view.state.doc.toString();
	console.log(doc);
	return doc;
}

let setTableAutocompletion = function (schema: { [table: string]: readonly string[] }) {
	language.reconfigure(sql({ upperCaseKeywords: true, schema: schema }));
}

declare global {
	interface Window
	{
		appendSqlEditor: any;
		getEditorContent: any;
		setTableAutocompletion: any;
	}
}

window.setTableAutocompletion = setTableAutocompletion;
window.appendSqlEditor = appendSqlEditor;
window.getEditorContent = getEditorContent;
