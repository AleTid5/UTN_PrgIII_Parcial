<template>
  <div class="content">
    <div class="md-layout">
      <el-select v-model="magicianSelected" placeholder="Seleccionar Mago" filterable class="el-col-12">
        <el-option
                v-for="magician in magicians"
                :key="magician.id"
                :label="magician.name"
                :value="magician.id">
        </el-option>
      </el-select>
      <div class="el-col-12" ref="spellOfMagician">
        <div v-if="spell.length !== 0">
          <div class="el-col-24" v-for="magicianSpell in magicianSpells">
            Hechizo: {{ magicianSpell.name }}: "{{ magicianSpell.description }}"<br>
            Es vencido por: {{ magicianSpell.spellDefeat.name }}: "{{ magicianSpell.spellDefeat.description }}"
            <hr>
          </div>
        </div>
      </div>
      <br><br><br><br>
      <div class="el-col-24">
        <h3 class="el-col-24 text-center">Crear Mago</h3>
        <div class="el-col-8">
          <el-input placeholder="Nombre de mago" v-model="form.name"></el-input>
        </div>
        <div class="el-col-8">
          <el-select v-model="form.house" placeholder="Seleccionar Casa" class="el-col-24">
            <el-option
                    v-for="house in houses"
                    :key="house.id"
                    :label="house.description"
                    :value="house.id">
            </el-option>
          </el-select>
        </div>
        <div class="el-col-8">
          <div class="el-col-8">&nbsp;</div>
          <div class="el-col-16">
          <el-checkbox-group v-model="form.spells">
            <el-checkbox v-for="spell in spells" :label="spell.id" :key="spell.id" class="el-col-24" :title="'Es vencido por: ' + spell.spellDefeat.name">{{ spell.name }}</el-checkbox>
          </el-checkbox-group>
        </div>
        </div>
      </div>
      <div class="el-col-24 text-center">
        <el-button type="success" plain @click="saveMagician()">Guardar!</el-button>
      </div>
    </div>
  </div>
</template>

<script>
  import api from "@/api";

  export default {
    async created() {
      this.magicians = await api.getMagicians();
      this.houses = await api.getMagiciansData(1);
      this.spells = await api.getMagiciansData(2);
    },

    computed: {
      spell() {
        return this.magicianSpells;
      }
    },

    data() {
      return {
        houses: [],
        spells: [],
        magicians: [],
        magicianSelected: null,
        magicianSpells: [],
        form: {
          name: null,
          house: null,
          spells: []
        }
      }
    },

    methods: {
      saveMagician() {
        api.saveMagician(this.form);
      }
    },

    watch: {
      magicianSelected(id) {
        const magician = this.magicians.filter(magician => magician.id === id).shift();
        this.magicianSpells = magician.spells;
      }
    }
  };
</script>
