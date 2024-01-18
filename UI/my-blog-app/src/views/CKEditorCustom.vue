<template>
  <ckeditor
    :editor="editor"
    v-model="editorData"
    :config="editorConfig"
  ></ckeditor>
</template>

<script>
import ClassicEditor from "@ckeditor/ckeditor5-build-classic";
import { defineComponent, ref } from "vue";
import { uploadImage } from "../utils/functions";

const editorData = ref("<p>Content of the editor.</p>");

function uploader(editor) {
  editor.plugins.get("FileRepository").createUploadAdapter = (loader) => {
    return new uploadImage(loader);
  };
}

export default defineComponent({
  setup() {
    return {
      editor: ClassicEditor,
      editorData,
      editorConfig: {
        // The configuration of the editor.
        extraPlugins: [uploader],
      },
    };
  },
});
</script>
