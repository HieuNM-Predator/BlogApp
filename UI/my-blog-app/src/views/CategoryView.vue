<template>
  <app-header />
  <v-container>
    <!-- Snack bar -->
    <v-snackbar
      v-model="snackbar"
      :timeout="2000"
      :color="color"
      rounded="pill"
      variant="elevated"
      position="relative"
      top="10"
    >
      {{ message }}
      <template v-slot:actions>
        <v-btn variant="text" @click="snackbar = false">
          <v-icon>mdi-close</v-icon>
        </v-btn>
      </template>
    </v-snackbar>
    <!-- Table -->
    <v-data-table :headers="headers" :items="categories" fixed-header>
      <template v-slot:top>
        <v-toolbar flat>
          <v-toolbar-title>Categories</v-toolbar-title>
          <v-spacer></v-spacer>
          <v-dialog v-model="dialog" max-width="500px">
            <template v-slot:activator="{ props }">
              <v-btn color="primary" dark class="mb-2" v-bind="props">
                New Item
              </v-btn>
            </template>
            <v-card>
              <v-form ref="form" @submit.prevent="onSubmit">
                <v-card-title>
                  <span class="text-h5">{{ formTitle }}</span>
                </v-card-title>

                <v-card-text>
                  <v-container>
                    <v-row>
                      <v-col cols="12">
                        <v-text-field
                          v-if="editedIndex !== -1"
                          v-model="editedItem.id"
                          variant="solo"
                          label="ID"
                          disabled
                        ></v-text-field>
                      </v-col>
                      <v-col cols="12">
                        <v-text-field
                          v-model="editedItem.name"
                          variant="solo"
                          label="Name"
                          :rules="NameRules"
                        ></v-text-field>
                      </v-col>
                      <v-col cols="12">
                        <v-text-field
                          v-model="editedItem.urlHandle"
                          variant="solo"
                          label="Url Handle"
                          :rules="UrlHandleRules"
                        ></v-text-field>
                      </v-col>
                    </v-row>
                  </v-container>
                </v-card-text>

                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn color="blue-darken-1" variant="text" @click="close">
                    Cancel
                  </v-btn>
                  <v-btn color="blue-darken-1" variant="text" type="submit">
                    Save
                  </v-btn>
                </v-card-actions>
              </v-form>
            </v-card>
          </v-dialog>
          <v-dialog v-model="dialogDelete">
            <v-card class="mx-auto">
              <v-card-title class="text-h5 text-truncate"
                >Are you sure you want to delete this category:
                {{ editedItem.name }} ?</v-card-title
              >
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="blue-darken-1" variant="text" @click="closeDelete"
                  >Cancel</v-btn
                >
                <v-btn
                  color="blue-darken-1"
                  variant="text"
                  @click="deleteItemConfirm"
                  >OK</v-btn
                >
                <v-spacer></v-spacer>
              </v-card-actions>
            </v-card>
          </v-dialog>
        </v-toolbar>
      </template>
      <template v-slot:[`item.actions`]="{ item }">
        <v-icon size="small" class="me-2" @click="updateItem(item)">
          mdi-pencil
        </v-icon>
        <v-icon size="small" @click="deleteItem(item)"> mdi-delete </v-icon>
      </template>
      <!-- <template v-slot:no-data>
        <v-btn color="primary" @click="initialize"> Reset </v-btn>
      </template> -->
    </v-data-table>
  </v-container>
</template>

<script setup>
import AppHeader from "@/components/AppHeader.vue";

import { ref, computed, nextTick, watch } from "vue";
import axios from "axios";

const headers = ref([
  {
    title: "Id",
    key: "id",
    sortable: false,
  },
  {
    title: "Name",
    key: "name",
  },
  {
    title: "Url Handle",
    key: "urlHandle",
  },
  {
    title: "Actions",
    key: "actions",
    sortable: false,
  },
]);
const categories = ref([]);

//Validation
const form = ref(null);
const NameRules = [
  (value) => {
    if (value) return true;
    return "You must enter category name.";
  },
];

const UrlHandleRules = [
  (value) => {
    if (value) return true;
    return "You must enter category's url handle";
  },
];

const editedIndex = ref(-1);
const editedItem = ref({});
const formTitle = computed(() => {
  return editedIndex.value === -1 ? "New Category" : "Edit Category";
});

//Đóng mở dialog
const dialog = ref(false);
const dialogDelete = ref(false);

const close = async () => {
  dialog.value = false;
  await nextTick(() => {
    editedIndex.value = -1;
    editedItem.value = {};
  });
};

const closeDelete = async () => {
  dialogDelete.value = false;
  await nextTick(() => {
    editedIndex.value = -1;
    editedItem.value = {};
  });
};

watch(dialog, (value) => {
  value || close();
});

watch(dialogDelete, (value) => {
  value || closeDelete();
});

watch(categories, (value) => {
  value || getAllCategories();
});

//Đóng mở snackbar
const snackbar = ref(false);
const message = ref("");
const color = ref("");
const showSnackbar = (msg, colour) => {
  message.value = msg;
  color.value = colour;
  snackbar.value = true;
};

// API get all
const getAllCategories = () => {
  axios
    .get("https://localhost:7200/api/Categories")
    .then((response) => {
      console.log(response);
      categories.value = response.data;
    })
    .catch((err) => {
      console.log(err);
    });
};
//Get all categories
//onMounted(getAllCategories);
getAllCategories();

const updateItem = (item) => {
  console.log(item);
  editedIndex.value = item.id;
  editedItem.value = Object.assign({}, item);
  dialog.value = true;
  // console.log(editedItem);
};

const deleteItem = (item) => {
  console.log(item);
  editedIndex.value = item.id;
  editedItem.value = Object.assign({}, item);
  dialogDelete.value = true;
  // console.log(editedItem.value);
  // console.log(editedIndex);
};

// Call API
const createCategory = (item) => {
  const category = {
    Name: item.name,
    UrlHandle: item.urlHandle,
  };
  axios
    .post("https://localhost:7200/api/Categories", category)
    .then((response) => {
      console.log(response);
      showSnackbar("Add category successfully!", "success");
      getAllCategories();
    })
    .catch((err) => {
      showSnackbar("Error", "red");
      console.log(err);
    });
};

const editCategory = (categoryId, params) => {
  axios
    .put(`https://localhost:7200/api/Categories/${categoryId}`, { ...params })
    .then((response) => {
      console.log(response.data);
      showSnackbar("Edit category successfully", "success");
      getAllCategories();
    })
    .catch((err) => {
      showSnackbar("Error", "red");
      console.log("Edit Category Error: ", err);
    });
};

const deleteCategory = (categoryId) => {
  axios
    .delete(`https://localhost:7200/api/Categories/${categoryId}`)
    .then((response) => {
      console.log(response);
      showSnackbar("Delete category successfully", "success");
      getAllCategories();
    })
    .catch((err) => {
      showSnackbar("Error", "red");
      console.log(" Delete ERROR: ", err);
    });
};

const deleteItemConfirm = () => {
  deleteCategory(editedItem.value.id);
  closeDelete();
};

const onSave = () => {
  if (editedIndex.value > -1 || editedIndex.value !== -1) {
    // Object.assign(this.desserts[this.editedIndex], this.editedItem);
    // console.log(editedItem);
    editCategory(editedItem.value.id, editedItem.value);
    console.log(editedIndex);
  } else {
    createCategory(editedItem.value);
  }
  close();
};

const onSubmit = async () => {
  const { valid } = await form.value.validate();
  if (valid) {
    onSave();
  }
};
</script>

<style lang="css" scoped></style>
